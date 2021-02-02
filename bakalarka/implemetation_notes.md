# Implementation notes

## Random Generator Alg implementation

https://en.wikipedia.org/wiki/Xorshift

https://en.wikipedia.org/wiki/Pseudorandom_number_generator

https://en.wikipedia.org/wiki/Middle-square_method#Middle_Square_Weyl_Sequence_PRNG

https://en.wikipedia.org/wiki/Weyl_sequence

https://en.wikipedia.org/wiki/Mersenne_Twister

http://prng.di.unimi.it/

https://peteroupc.github.io/random.html#Nondeterministic_Sources_and_Seed_Generation

https://github.com/peteroupc/peteroupc.github.io/blob/master/randomtest.md

https://www.pcg-random.org/posts/how-to-test-with-practrand.html

## IgnoreFaker class

Volat z ctoru `BaseFakeru` metodu, co používá reflexion, aby ososala případné `FakerIgnore` atributy z položek třídy plněné pseudonáhodným obsahem, ctor zpomalí 100x (viz benchmarks). 

**Benchmark, výsledky předtím, než byl implementován `IgnoreFaker`**

``` ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.746 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.101
  [Host]     : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT


```

| Method              |        Mean |     Error |    StdDev | Ratio | RatioSD | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
| ------------------- | ----------: | --------: | --------: | ----: | ------: | ---: | -----: | ----: | ----: | --------: |
| WithoutIgnoreNOScan |    253.3 ns |   4.82 ns |   8.31 ns |  0.01 |    0.00 |    1 | 0.4282 |     - |     - |     672 B |
| WithoutIgnoreScan   | 21,394.1 ns | 301.76 ns | 267.51 ns |  0.99 |    0.02 |    2 | 2.9297 |     - |     - |    4617 B |
| WithIgnoreScan      | 21,605.3 ns | 358.42 ns | 335.27 ns |  1.00 |    0.00 |    2 | 2.9297 |     - |     - |    4617 B |


```csharp
//ctor added to BaseFaker to allow previous benchmark to run
public BaseFaker(bool scan)
{
    this.Random = new RandomGenerator();
    if (scan)
    {
        ScanIgnoreAttriutes();
    }
}
```

```csharp
//used benchmark
public class WithIgnore
    {
        public int Int;
        public byte Byte;
        [FakerIgnore]
        public short Short { get; set; } = 73;
        public DateTime DateTime { get; set; }
        [FakerIgnore]
        public double Double = 42.73;
        public Guid Guid;
        [FakerIgnore]
        public string IgnoredString { get; set; } = "IGNORED";
        [FakerIgnore]
        public int IgnoredInt = 42;
    }

    public class WithIgnoreFaker : BaseFaker<WithIgnore>
    {
        public WithIgnoreFaker(bool scan) :base(scan)
        {

        }
    }

    public class WithoutIgnore
    {
        public int Int;
        public byte Byte;
        public short Short { get; set; } = 73;
        public DateTime DateTime { get; set; }
        public double Double = 42.73;
        public Guid Guid;
        public string IgnoredString { get; set; } = "IGNORED";
        public int IgnoredInt = 42;
    }

    public class WithoutIgnoreFaker : BaseFaker<WithIgnore>
    {
        public WithoutIgnoreFaker(bool scan) : base(scan)
        {

        }
    }

    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class IgnoreBenchmarks
    {
        [Benchmark(Baseline =true)]
        public void WithIgnoreScan()
        {
            //scanning is carried out, class contains FakerIgnore attributes
            new WithIgnoreFaker(true);
        }

        [Benchmark]
        public void WithoutIgnoreScan()
        {
            //scanning is carried out, class doesn't contain FakerIgnore attributes
            new WithoutIgnoreFaker(true);
        }

        [Benchmark]
        public void WithoutIgnoreNOScan()
        {
            //no scanning carried out
            new WithoutIgnoreFaker(false);
        }
    }
```



`FakerIgnore` atributy jsou používány v uživatelem definovaných třídách, které jsou již implementovány s tím, že budou vyplňovány pseudonáhodným obsahem a tento fakt je jedním z hlavních důvodů jejich existence. Takových tříd ale bude pravděpodobně spíše menšina mezi všemi uživatelem definovanými třídami, pro které bude `Faker` používán. Lze předpokládat, že nejčastěji bude `Faker` nadefinován pro třídu, jejíž primární účel nemá s vyplňováním pseudonáhodným obsahem nic společného a pseudonáhodného obsahu je využito např. pro testování funkčnosti či výkonosti.  Nezdá se mi proto smysluplné kvůli okrajovému případu použití s `FakeIgnore`atributy zpomalit mnohem frekventovanější způsob využití bez těchto atributů. 

"Skenování" uživatelem definované třídy, jestli obsahuje `FakerIgnor` atributy je třeba provádět v ctoru `Fakeru`, od kterého uživatelův specializovaný `Faker` dědí. Kdyby automaticky probíhalo v ctoru `BaseFakeru`, došlo by k popisovanému zpomalení base case na úkor corner case.

Jelikož ale ctor předka je  volán na začátku ctoru potomka, nelze to, jestli má ke skenování dojít, řídit nějakým flagem na `BaseFakeru`, jež by se podobně jako `FillEmptyMembers` flag nastavoval z ctoru uživatelem definovaného potomka `BaseFakeru`, protože k nastavení tohoto flagu by došlo až po provedení těla ctoru `BaseFakeru`, kde by ale už bylo třeba hodnoty flagu využít k rozhodnutí, zda skenovat, či nikoli. 

Situaci by bylo možné vyřešit parametrem ctoru `BaseFakeru` (`bool`, co by určoval, zda skenovat, či nikoli). Pak by ale každý ctor uživatelova specializovaného `Fakeru`, který by měl vytvářet instance `Fakeru`, co respektují `FakerIgnore` atributy,  musel  přijímat `bool` parametr (např. `bool doScanning`) a použít konstrukci `:base(doScanning)`. Tento přístup mi přijde syntakticky neohrabaný a netransparentní. Navíc by tak mohly vznikat instance stejného typu `Fakeru`, z nichž některé by `FakerIgnore` atributy respektovaly a některé nikoli, což je matoucí a nekonzistentní.

Proto jsem se rozhodla naimplementovat `IgnoreFaker <TClass>` typ poděděný od `BaseFaker<TClasss>`. V ctoru `IgnoreFakeru` je volán příslušný ctor `BaseFakeru` a navíc je zde provedeno skenování atributů. `BaseFaker` tedy není sám o sobě skenováním zatížen a usage base case není zbytečně zpomalen. Použití `IgnoreFakeru` je stále stejně jednoduché jako použití `BaseFakeru`. 

Nevýhodou tohoto přístupu oproti skenování atributů přímo v ctoru  `BaseFakeru` je menší transparentnost použití `FakerIgnore` atributů. Uživatel, jež si není vědom existence a účelu `IgnoreFakeru` bude zmaten, že jeho `FakerIgnore` atributy nemají ve `Fakeru` poděděném přímo od `BaseFakeru` žádný účinek.  

**Benchmark , `IgnoreFaker` vs `BaseFaker`**

``` ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.746 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.101
  [Host]     : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT


```

| Method                   |        Mean |     Error |    StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
| ------------------------ | ----------: | --------: | --------: | ---: | -----: | ----: | ----: | --------: |
| WithoutIgnoreBaseFaker   |    251.7 ns |   5.03 ns |  11.36 ns |    1 | 0.4282 |     - |     - |     672 B |
| WithIgnoreBaseFaker      |    252.6 ns |   4.97 ns |  10.37 ns |    1 | 0.4282 |     - |     - |     672 B |
| WithoutIgnoreIgnoreFaker | 21,391.5 ns | 409.87 ns | 739.09 ns |    2 | 2.9297 |     - |     - |    4617 B |
| WithIgnoreIgnoreFaker    | 22,056.8 ns | 428.68 ns | 641.63 ns |    3 | 2.9297 |     - |     - |    4617 B |

```csharp
//used benchmark 
public class WithIgnore
    {
        public int Int;
        public byte Byte;
        [FakerIgnore]
        public short Short { get; set; } = 73;
        public DateTime DateTime { get; set; }
        [FakerIgnore]
        public double Double = 42.73;
        public Guid Guid;
        [FakerIgnore]
        public string IgnoredString { get; set; } = "IGNORED";
        [FakerIgnore]
        public int IgnoredInt = 42;
    }

    public class WithIgnoreBaseFaker : BaseFaker<WithIgnore> { }

    public class WithIgnoreIgnoreFaker : IgnoreFaker<WithIgnore> { }
 

    public class WithoutIgnore
    {
        public int Int;
        public byte Byte;
        public short Short { get; set; } = 73;
        public DateTime DateTime { get; set; }
        public double Double = 42.73;
        public Guid Guid;
        public string IgnoredString { get; set; } = "IGNORED";
        public int IgnoredInt = 42;
    }

    public class WithoutIgnoreBaseFaker : BaseFaker<WithIgnore> { }

    public class WithoutIgnoreIgnoreFaker : IgnoreFaker<WithIgnore> { }

    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class IgnoreBenchmarks
    {
        [Benchmark]
        public void WithIgnoreBaseFaker()
        {
            // no scanning is carried out, class contains FakerIgnore attributes
            new WithIgnoreBaseFaker();
        }

        [Benchmark]
        public void WithIgnoreIgnoreFaker()
        {
            //scanning is carried out, class contains FakerIgnore attributes
            new WithIgnoreIgnoreFaker();
        }

        [Benchmark]
        public void WithoutIgnoreBaseFaker()
        {
            // no scanning is carried out, class doesn't contain FakerIgnore attributes
            new WithoutIgnoreBaseFaker();
        }

        [Benchmark]
        public void WithoutIgnoreIgnoreFaker()
        {
            //scanning is carried out, class doesn't contain FakerIgnore attributes
            new WithoutIgnoreIgnoreFaker();
        }
    }
```

