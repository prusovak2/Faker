# Faker

## Uživatelská dokumentace

`Faker` je generátor pseudonáhodného obsahu objektů v C#. Lze ho využít k vyplnění instancí uživatelem definovaných tříd daty, která jsou pseudonáhodná, ale relevantní v uživatelem specifikovaném kontextu. Umožňuje uživateli nakonfigurovat, jakým způsobem mají být instance jeho třídy vyplňovány a následně generovat (případně velké) množství takovýchto instancí. `Faker` lze využít například ke generování testovacích dat či dat pro vytváření benchmarks.

 ### Návod k použití

#### `RuleFor`, `Generate`, `Populate`

API pro vyplňování instancí pseudonáhodným obsahem poskytuje třída `BaseFaker<TClass>`.  Pro nakonfigurování `Fakeru` pro svůj typ musí uživatel naimplementovat jednoduchou třídu, která bude potomkem třídy `BaseFaker` specializované na uživatelem definovaný typ, jehož instance chce plnit pseudonáhodným obsahem (např. `MyTypeFaker: BaseFaker<MyType>`).  Pro vyplnění položek základních datových typů (za které jsou v rámci tohoto projektu považovány `byte`, `sbyte`, `short`, `ushort`, `int`, `uint`, `long`, `ulong`, `float`, `double`, `decimal`, `char`, `bool`, `string`, `DateTime` a `Guid`) je třeba v konstruktoru MyTypeFakeru třeba volat metodu `RuleFor`, jejímž prvním parametrem je lambda funkce vracející položku, kterou chceme vyplnit a druhým parametrem lambda funkce na konkrétní metodu na třídě `RandomGenerator`, jež chceme pro vyplnění položky použít.

Předpokládejme například následující uživatelem definovanou třídu.

```csharp
public class Person
{
    public Guid Id { get; set; }
    public int Age { get; set; }
    public string Name { get; set; }
    public bool IsAwesome { get; set; }
    //+ corresponding override ToString()
}
```

`Faker` pro ni může vypadat například takto:

```csharp
public class BasicPersonFaker: BaseFaker<Person>
{
    public BasicPersonFaker()
    {
        RuleFor(person => person.Id, rg => rg.Random.Guid());
        RuleFor(p => p.Age, rg => rg.Random.Int(0, 100));
        RuleFor(p => p.Name, rg => rg.String.Letters(30));
        RuleFor(p => p.IsAwesome, rg => rg.Random.Bool());
    }
}
```

Pro vytvoření instance uživatelem definované třídy a její vyplnění pseudonáhodným obsahem lze volat metodu `Generate()`.

```csharp
BasicPersonFaker basicPersonFaker = new BasicPersonFaker();
for (int i = 0; i < 5; i++)
{
    Person p = basicPersonFaker.Generate();
    Console.WriteLine(p);
}
```

Příklad výstupu:

```
Person: Id:e9eae384-42a6-d96d-444f-c9d627002044, Age:35, Name:mWbLEGbuaeExlRBySkKbUlwOBaMwXy, IsAwesome:False
Person: Id:586c5f69-725e-cd7d-9775-9aba4393864b, Age:48, Name:XfkjHHvmsdIVdBQpEJcfaiVFUpdoiF, IsAwesome:True
Person: Id:3781334b-c4ee-c900-5411-4feba6f5d1b3, Age:18, Name:driXvSijRpsJXFtiUEvlGPxJduwgAq, IsAwesome:False
Person: Id:bbd9049c-245e-86a8-8b59-c47a4b89d90f, Age:81, Name:DLeAZFMEwjRkoCqLqaNtHdJQORvusD, IsAwesome:False
Person: Id:561f4e7a-4856-1a48-2fb8-8c35aeff3241, Age:25, 
```

Pro vyplnění již existující instance pseudonáhodným obsahem lze volat metodu `Populate()`.

```csharp
Person AnotherPerson = new Person();
basicPersonFaker.Populate(AnotherPerson);
```

Overload metody `Generate(object[] CtorParams)` umožňuje vytvářet instance daného uživatelem definovaného typu voláním konstruktoru s konkrétní podobou parametrů. Pokud `object[] CtorParams` obsahuje parametry, které neodpovídají parametrům žádného existujícího konstruktoru na daném typu, vyhodí toto volání metody `Generate` `ArgumentException`.

Obecně je efektivnější vyplňovat metodou `Populate` již existující instance, neboť se tento přístup vyhýbá volání konstruktoru s přes `reflection`.

`BaseFaker` lze specializovat pouze na referenční typy (tedy typy splňující `class` constraint).

Vyplňovat lze jak `properties` tak `fields`, ale pouze pokud jsou veřejné (`public`).

#### `SetFaker`

Obsahuje-li vyplňovaná třída položky jiného než jednoho ze základních typů, je pro tyto položky možné nastavit `InnerFaker` voláním metody `SetFaker()` v konstruktoru faker třídy pro daný typ. Instance `Fakeru`, specializovaného na typ položky, předaná metodě `SetFaker()`, je následně využívána pro generování obsahu položky. Kdykoli je zavoláno `Generate()` či `Populate()` na `Fakeru` pro nadřazenou třídu, jsou rekurzivně zavolány i `Generate()` či `Populate()` metody na všech `InnerFakerech` nastavených metodou `SetFaker()` pro tento vnější  `Faker`. Zda je na `InnerFakeru` volána `Generate` či `Populate` lze nastavit (viz níže).

Mějme následující uživatelem definované třídy.

```csharp
public class Storage
{
    public ValueClass Value { get; set; }
    public int Property { get; set; }
    public double Field;
}

public class ValueClass
{
    public int Number { get; set; }
    public byte SmallerNumber {get; set;}
}
```

Příklad implementace `Fakerů` pro dané typy:

```csharp
public class ValueClassFaker: BaseFaker<ValueClass>
{
    public ValueClassFaker()
    {
        RuleFor(val => val.Number, rg => rg.Random.IntEven());
        RuleFor(val => val.SmallerNumber, rg => rg.Random.Byte());
    }
}
public class StorageFaker: BaseFaker<Storage>
{
    public StorageFaker()
    {
        SetFaker(s => s.Value, new ValueClassFaker());
        RuleFor(s => s.Property, rg => rg.Random.Int(upper:42));
        RuleFor(s => s.Field, rg => rg.Random.Double());
    }
}
```

Příklad použití:

```csharp
StorageFaker storageFaker = new StorageFaker();
for (int i = 0; i < 5; i++)
{
    Storage s = storageFaker.Generate();
    Console.WriteLine(s);
}
```

Ukázka výstupu:

```csharp
Storage: Value: Number:-230878548, SmallerNumber:59, Property:-940491482, Field:0,7210118036722626
Storage: Value: Number:152173554, SmallerNumber:235, Property:-259550074, Field:0,41245330176316897
Storage: Value: Number:1150348440, SmallerNumber:225, Property:-1923769920, Field:0,8512031865874317
Storage: Value: Number:-83603322, SmallerNumber:32, Property:-508065788, Field:0,7453743063797382
Storage: Value: Number:182117732, SmallerNumber:45, Property:-1471228054, Field:0,3336193179180027
```

Metody `SetFaker` a `RuleFor` jsou typově bezpečné v tom smyslu, že nelze přiřadit `InnerFaker` k položce, jsou-li typy nekompatibilní (`InnerFaker` specializovaný na jiný typ než je typ položky). Stejně tak nelze nastavit metodu vracející typ, co nelze do položky přiřadit, jako `RuleFor` pro vyplnění dané položky. Tyto pokusy vedou k chybám při kompilaci. Následující kód tedy nezkompiluje.

```csharp
public class FlawedStorageFaker: BaseFaker<Storage>
{
    public FlawedStorageFaker()
    {
        //won't compile
        //trying to fill the int property by a string value
        RuleFor(s => s.Property, rg => rg.String.String());
        //trying to set faker specialized on another type (Person)
        //than is the type of the property (ValueClass)
        SetFaker(s => s.Value, new BasicPersonFaker());
    }
}
```

#### `FillEmptyMembers`

Pro usnadnění implementace `Fakerů` pro uživatelem definované typy nabízí `BaseFaker` možnost vyplňovat automaticky všechny položky základních typů (viz seznam výše), pro které není specifikováno `RuleFor`, pomocí defaultní randomizační funkce, kterou `RandomGenerator` pro daný typ disponuje (například pro `int` se jedná o volání `randomGeneratorInstance.Random.Int()`, která vrací náhodný integer z intervalu [`int.MinValue`, `int.Maxvalue`]). 

Pro aktivování této feature  je třeba nastavit na `Fakeru` položku `FillEmptyMembers` na `UnfilledMembers.DefaultRandomFunc` . (Defaultní hodnota této položky je `UnfilledMembers.LeaveBlank`). `Faker` pro třídu `Person` ze začátku tohoto dokumentu tedy může vypadat i takto:

```csharp
public class SimplePersonFaker : BaseFaker<Person>
{
    public SimplePersonFaker()
    {
        this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
    }
}
```

#### `CtorUsageFlag`

`CtorUsageFlag` položka na `BaseFakeru` umožňuje nastavit, jaká metoda (`Generate()`,`Generate(object[] CtorParams)` či `Populate()`) má být volána, když je `Faker` použit jako `InnerFaker`, tzn. byl  v konstruktoru nějakého jiného `Fakeru` metodou `SetFaker` nastaven jako `InnerFaker` pro nějakou položku. Je-li zvolena varianta `InnerFakerConstructorUsage.GivenParameters`, parametry pro konstruktor musí být předány v položce `CtorParameters`. Neodpovídají-li předané parametry žádnému overloadu konstruktoru pro daný typ, je po zavolání metody `Generate` vyhozena `FakerException`.

Tento flag je brán v potaz, když je `Faker` využit jako `InnerFaker`. Volá-li uživatel na `Fakeru` metodu `Generate` (či jiný ekvivalent) ručně,  `CtorUsageFlag` ani `CtorParameters` nemají na toto volání žádný vliv.

#### `BaseFaker` konstruktor

Je možné, aby vícero `Fakerů` sdílelo jednu instanci `RandomGeneratoru` (což je třída, jež poskytuje metody pro generování náhodných hodnot). Lze toho docílit předáním instance `RandomGeneratoru` konstruktoru `Fakeru`. Speciálně lze doporučit, aby všechny `InnerFakery` sdíleli stejnou instanci `RandomGeneratoru` jako jejich nadřazený `Faker` a tím byla ušetřena paměť za vícero instancí `RandomGeneratoru`, viz příklad.

```csharp
    public class ValueClassFakerShared : BaseFaker<ValueClass>
    {
        public ValueClassFakerShared(RandomGenerator random):base(random)
        {
            RuleFor(val => val.Number, rg => rg.Random.IntEven());
            RuleFor(val => val.SmallerNumber, rg => rg.Random.Byte());
        }
    }
    public class StorageFakerShared : BaseFaker<Storage>
    {
        public StorageFakerShared()
        {
            SetFaker(s => s.Value, new ValueClassFakerShared(this.Random));
            RuleFor(s => s.Property, rg => rg.Random.Int(upper: 42));
            RuleFor(s => s.Field, rg => rg.Random.Double());
        }
    }
```



### `RandomGenerator`

Třída `RandomGenerator` obsahuje řadu metod, generujících náhodné hodnoty různých typů. Mnoho z těchto metod disponuje overloady, které umožňují například učit, z jakého intervalu chceme náhodnou číselnou hodnotu vybrat, či jak dlouhou kolekci náhodných hodnot chceme vygenerovat a zda má být předaná velikost použita přesně či jako horní mez velikosti kolekce. Více viz dokumentační komentáře ve zdrojovém kódu.

Každá instance třídy `BaseFaker` (či odvozené třídy), disponuje referencí na instanci `RandomGeneratoru`, která jí poskytuje náhodné hodnoty pro vyplnění položek objektů.

`RandomGenerator` lze však používat i nezávisle na `BaseFaker`.

```csharp
RandomGenerator rg = new RandomGenerator(); 
DateTime date = rg.Random.DateTime(new DateTime(2000, 1, 1), new DateTime(2020, 1, 1));
IEnumerable<sbyte>  sbytes = rg.Enumerable.Sbyte(42);
decimal d = rg.Random.Decimal();
```

Pokud chce uživatel duplikovat stejnou posloupnost náhodných hodnot (například za účelem zopakovaní případu, kdy v jeho programu došlo k výjimce), může zjistit seed `RandomGeneratoru`, který tuto posloupnost vygeneroval (položka `Seed`). Tento seed pak lze předat konstruktoru `RandomGeneratoru` a vytvořit tak novou instanci, která bude produkovat stejné posloupnosti náhodných hodnot. 

#### Přehled podporovaných náhodných entit

#### RandomGenerator.

##### Random.

 * Double
 * Float
 * Decimal
 * Int
 * Uint
 * Short
 * Ushort
 * Sbyte
 * Byte
 * Long
 * Ulong
 * EvenInt
 * OddInt
 * Bool
 * Guid
 * DateTime
 * Char
 * String

##### Char.

* Char
* Digit
* LowerCaseLetter
* UpperCaseLetter
* Letter
* Alphanumeric
* Ascii
* Vowel
* Consonant
* HexadecimalDigit
* WhiteSpace

##### String.

* String
* LowerCaseLetters
* UpperCaseLetters
* Letters
* Aplhanumeric
* Hexadecimal

##### Enumerable.

 * Double
 * Float
 * Decimal
 * Int
 * Uint
 * Short
 * Ushort
 * Sbyte
 * Byte
 * Long
 * Ulong
 * EvenInt
 * OddInt
 * Bool
 * Guid
 * DateTime
 * Char
 * String

###### RandomCollection<TMember>

###### RandomList<TMember>

###### RandomEnumerable<TMember>