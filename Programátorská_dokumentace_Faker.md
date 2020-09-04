# Faker

## Programátorská dokumentace

`Faker` je generátor pseudonáhodného obsahu objektů v C#. Lze ho využít k vyplnění instancí uživatelem definovaných tříd daty, která jsou pseudonáhodná, ale relevantní v uživatelem specifikovaném kontextu. Umožňuje uživateli nakonfigurovat, jakým způsobem mají být instance jeho třídy vyplňovány a následně generovat (případně velké) množství takovýchto instancí. `Faker` lze využít například ke generování testovacích dat či dat pro vytváření benchmarks.

### Generátor náhodných čísel

Základem, na kterém stojí fungování celého programu, je třída `Xoshiro256starstar`.  Jak název napovídá, třída obsahuje implementaci algoritmu pro generování pseudonáhodných čísel `Xoshiro256**`, jehož tvůrci jsou David Blackman a Sebastiano Vigna (http://prng.di.unimi.it/xoshiro256starstar.c). Ač algoritmus jistě není bezchybný (https://www.pcg-random.org/posts/a-quick-look-at-xoshiro256.html), zdá se mi pro účely tohoto programu dostačující a jeho rychlost je bezpochyby výhodou. Jistě lze ale implementací nějakého jiného (vícero jiných) PNRG projekt v budoucnu vylepšit a rozšířit. `Xoshiro256**` funguje na základě sekvence bitových shiftů, xorů a rollů. Má 64 bitů vnitřního stavu, který musí být inicializován nenulovými hodnotami. Za účelem inicializace vnitřního stavu doporučují tvůrci použít algoritmus `Splitmix64`,  který je v projektu tudíž naimplementován a takto využit (http://prng.di.unimi.it/splitmix64.c).

#### WeylSequenceSeedCounter

V .Net Framework je seed PRNG dostupného v třídě System.Random založený na aktuálním čase. Jelikož se ale čas aktualizuje diskrétně, mohou mít dva krátce po sobě vytvořené instance této třídy stejný seed a produkovat tedy stejné sekvence pseudonáhodných čísel. (https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netcore-3.1). Aby se tomuto problému moje implementace PRGN vyhnula, používá při vytváření seed kromě aktuálního času ještě counter založený na Weyl sequence (https://en.wikipedia.org/wiki/Weyl_sequence), jejíž prvky by měly mít rozložení podobné uniformnímu rozložení (tedy být v daném univerzu rozmístěny rovnoměrně). V diskrétní verzi se jedná o posloupnost násobků vysokého celého čísla, které je nesoudělné s modulem (v tomto případě s 2^64 neboť používáme proměnné typu ulong).  Třída `Splitmix64` tedy disponuje statickým counterem, který je vždy při vytvoření instance této třídy inkrementován o konkrétní konstantu a inkrementovaná verze je následně použita k vytvoření seed pro Splitmix64, který vygeneruje počáteční stav pro `Xoshiro256**`, jež následně generuje všechna náhodná čísla využitá v programu. Counter je opatřen lockem, aby fungoval korektně i při použití z více vláken.

#### Generování floating point čísel

Tvůrci algoritmu `Xoshiro256**` uvádějí, že rozumné kvality náhodnosti dosahuje horních 53 bitů 64-bitového čísla vráceného jejich algoritmem. Těchto 53 bitů je v mém programu využito jako mantisa double z intervalu [0,1). Na generování doublů z tohoto intervalu je následně založeno generování všech ostatních náhodných entit (kromě floatů, které jsou generovány přímo, podobným způsobem jako doubly). Pro získání doublu z jiného intervalu je tento interval namapován na interval [0,1). Celočíselné typy jsou zaokrouhlovány a přetypovávány. Jelikož je však vždy číslo původního typu vygenerováno z intervalu s koncovými body předanými jako hodnoty cílového typu, vygenerované hodnota se do cílového typu vždy vejde a přetypování tedy nemůže vést k InvalidCastException.

### Třída `RandomGenerator`

Tato třída disponuje veškerými metodami pro generování náhodných entit, co jsou v projektu dostupné. Implementace těchto metod jsou rozděleny do vnořených tříd RandomString, RandomChar, RandomBasicTypes a RandomEnumerable. Tento návrh má uživateli zprostředkovat příjemnější práci s náhodným generátorem, kdy za užití tečkové notace nejprve zvolí kategorii metod, následně díky IntelliSence uvidí, jaké metody se v této konkrétní kategorii nachází.

![IntelliSenceExample](D:\MFF\LS_2020\cSharp\Faker\IntellisenceExample.png)

#### RandomEnumerable

Třída RandomGenerator.RandomEnumerable poskytuje metody, jež vrací RandomEnumerable od všech typů, co jsou v uživatelské části dokumentace uvedeny jako základní. Tyto metody pouze vytváří příjemnější API a interně volají metody RandomGenerator.GenericEnumerable<TMemeber> či RandomGenerator.InfiniteGenericEnumerable<TMember>. Generické varianty RandomEnumerable metod jsou velice silným nástrojem, který umožňuje vytvořit Enumerable libovolného typu, dokud pro ten typ existuje (randomizační) funkce s odpovídajícím formátem parametrů, co je využita k produkování hodnot v Enumerable. 

```csharp
public IEnumerable<TMember> GenericEnumerable<TMember>(Func<int, bool, 			TMember> randomFunc, int countOuter, bool preciseOuter, int 				countInnerCollection, bool preciseInnerCollection)
        {
            int countToUse = this.CountToUse(countOuter, preciseOuter);
            for (int i = 0; i < countToUse; i++)
            {
                TMember next = (TMember)randomFunc(countInnerCollection, 										preciseInnerCollection);
                yield return next;
            }
        }
```

Ač jsou generické varianty v současném okamžiku součástí veřejného API, protože je s jejich využitím například možno vytvořit Enumerable náhodných alfanumerických stringů, což současné API jinak neumožňuje, je jejich volání poněkud krkolomné a stojí za uvážení jejich označení jako internal. Jsou však nedocenitené při implementaci jiných metod  na RandomGeneratoru.

