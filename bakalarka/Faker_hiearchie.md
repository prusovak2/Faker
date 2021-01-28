# Faker - Hiearchie

## Done

### `RuleFor()`

**Nastavení pravidel pro vyplnění položek základních typů. Třeba volat z konstruktoru MyTypeFakeru.**

Pro nakonfigurování `Fakeru` pro svůj typ musí uživatel naimplementovat jednoduchou třídu, která bude potomkem třídy `BaseFaker` specializované na uživatelem definovaný typ, jehož instance chce plnit pseudonáhodným obsahem (např. `MyTypeFaker: BaseFaker<MyType>`).  Pro vyplnění položek základních datových typů (za které jsou v rámci tohoto projektu považovány `byte`, `sbyte`, `short`, `ushort`, `int`, `uint`, `long`, `ulong`, `float`, `double`, `decimal`, `char`, `bool`, `string`, `DateTime` a `Guid`) je třeba v konstruktoru MyTypeFakeru třeba volat metodu `RuleFor`, jejímž prvním parametrem je lambda funkce vracející položku, kterou  chceme vyplnit a druhým parametrem lambda funkce na konkrétní metodu na  třídě `RandomGenerator`, jež chceme pro vyplnění položky použít.

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

### `Generate()`

**Vytvoření instance uživatelem definované třídy a její vyplnění pseudonáhodným obsahem.** 

Volána na instanci potomka BaseFakeru specializovaného na konkrétní uživatelem definovaný typ.

```csharp
BasicPersonFaker basicPersonFaker = new BasicPersonFaker();
Person p = basicPersonFaker.Generate();
```

### `Generate(object[] CtorParams)`

**Vytvoření instance uživatelem definované třídy voláním konstruktoru s konkrétní podobou parametrů a její vyplnění pseudonáhodným obsahem.** 

Pokud `object[] CtorParams` obsahuje parametry, které neodpovídají parametrům žádného existujícího konstruktoru na daném typu, vyhodí toto volání metody `Generate` `ArgumentException`.

Aby využití Fakeru pro danou uživatelem definovanou třídu nevyžadovalo přítomnost bezparametrického konstruktoru na dané třídě.

Volána na instanci potomka BaseFakeru specializovaného na konkrétní uživatelem definovaný typ.

```csharp
public class Person
{
    public Person(int age) {
        this.Age = age;
    }
    public Guid Id { get; set; }
    public int Age { get; set; }
    public string Name { get; set; }
    public bool IsAwesome { get; set; }
}

public class PersonFaker: BaseFaker<Person>
{
    public PersonFaker()
    {
        RuleFor(person => person.Id, rg => rg.Random.Guid());
        RuleFor(p => p.Name, rg => rg.String.Letters(30));
        RuleFor(p => p.IsAwesome, rg => rg.Random.Bool());
    }
}
```
```csharp
PersonFaker personFaker = new PersonFaker();
//create a person that is 42 years old
Person p = personFaker.Generate(42);
```

### `Populate(TClass instance)`

**Vyplnění již existující instance uživatelem definované třídy pseudonáhodným obsahem.** 

Obecně je efektivnější vyplňovat metodou `Populate` již existující instance, neboť se tento přístup vyhýbá volání konstruktoru s přes `reflection`.

```csharp
BasicPersonFaker basicPersonFaker = new BasicPersonFaker();
Person AnotherPerson = new Person();
basicPersonFaker.Populate(AnotherPerson);
```

### `SetFaker()`

**Nastavení pravidel pro vyplnění položek jiných než základních typů. Třeba volat z konstruktoru MyTypeFakeru.**

Obsahuje-li vyplňovaná třída položky jiného než jednoho ze základních typů, je pro tyto položky možné nastavit `InnerFaker` voláním metody `SetFaker()` v konstruktoru faker třídy pro daný typ. Instance `Fakeru`, specializovaného na typ položky, předaná metodě `SetFaker()`, je následně využívána pro generování obsahu položky. Kdykoli je zavoláno `Generate()` či `Populate()` na `Fakeru` pro nadřazenou třídu, jsou rekurzivně zavolány i `Generate()` či `Populate()` metody na všech `InnerFakerech` nastavených metodou `SetFaker()` pro tento vnější  `Faker`. Zda je na `InnerFakeru` volána `Generate` či `Populate` lze nastavit (viz níže).

```csharp
public class Person
{
    public Address address { get; set; }
    public Guid id { get; set; }
}

public class Address
{
    public int buildingNumber { get; set; }
    public string street { get; set; }
}

public class AddressFaker: BaseFaker<Address>
{
    public AddressFaker()
    {
        RuleFor(a => a.buildingNumber, rg => rg.Random.Int());
        RuleFor(a => a.street, rg => rg.Random.String());
    }
}
public class PersonFaker: BaseFaker<Person>
{
    public PersonFaker()
    {
        SetFaker(p => p.Address, new AddressFaker());
        RuleFor(p => p.id, rg => rg.Random.Guid());
    }
}
```

### `FillEmptyMembers` flag

**Vyplnění položek bez pravidla defaultní randomizační funkcí pro daný typ.**

Flag na Fakeru, když je v konstruktoru poděděného Fakeru nastaven na hodnotu `UnfilledMembers.DefaultRandomFunc`, bude `Faker` vyplňovat automaticky všechny položky základních typů (viz seznam výše), pro které není specifikováno `RuleFor`, pomocí defaultní randomizační funkce, kterou `RandomGenerator` pro daný typ disponuje. Defaultní hodnota této položky je `UnfilledMembers.LeaveBlank`.  Pro snadnou konfiguraci Fakeru bez nutnosti specifikovat mnoho předvídatelných pravidel.

```csharp
public class SimplePersonFaker : BaseFaker<Person>
{
    public SimplePersonFaker()
    {
        this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
    }
}
```

### `CtorUsageFlag`

**Která z metod  `Generate()`,`Generate(object[] CtorParams)` či `Populate()` má být na Fakeru volána, pokud je použit jako InnerFaker?**

Tento flag je brán v potaz, když je `Faker` využit jako `InnerFaker`. Volá-li uživatel na `Fakeru` metodu `Generate` (či jiný ekvivalent) ručně,  `CtorUsageFlag` ani `CtorParameters` nemají na toto volání žádný vliv.

Nastavován v konstruktoru poděděného Fakeru stejně jako `FillEmptyMembers`  flag.

Je-li zvolena varianta `InnerFakerConstructorUsage.GivenParameters`, parametry pro konstruktor musí být předány v položce `CtorParameters`. 

### Přetížení `BaseFaker` konstruktoru

Faker lze vytvořit s již existující instancí RandomGeneratoru.  Speciálně lze doporučit, aby všechny `InnerFakery` sdíleli stejnou instanci `RandomGeneratoru` jako jejich nadřazený `Faker` a tím byla ušetřena paměť za vícero instancí `RandomGeneratoru`, viz příklad.

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

### `RandomGenerator` lze používat i nezávisle na `BaseFaker`

```csharp
RandomGenerator rg = new RandomGenerator(); 
DateTime date = rg.Random.DateTime(new DateTime(2000, 1, 1), new DateTime(2020, 1, 1));
IEnumerable<sbyte>  sbytes = rg.Enumerable.Sbyte(42);
decimal d = rg.Random.Decimal();
```

#### `RandomEnumerable`

Ač jsou generické varianty v současném okamžiku součástí veřejného API, protože je s jejich využitím například možno vytvořit `Enumerable` náhodných alfanumerických stringů, což současné API jinak neumožňuje,  je jejich volání poněkud krkolomné a stojí za uvážení jejich označení  jako `internal`. Jsou však nedocenitelné při implementaci jiných metod na `RandomGeneratoru`.

### Notes

`BaseFaker` lze specializovat pouze na referenční typy (tedy typy splňující `class` constraint).

Vyplňovat lze jak `properties` tak `fields`, ale pouze pokud jsou veřejné (`public`).

Metody `SetFaker` a `RuleFor` jsou typově bezpečné v tom smyslu, že nelze přiřadit `InnerFaker` k položce, jsou-li typy nekompatibilní (`InnerFaker` specializovaný na jiný typ než je typ položky). Stejně tak nelze  nastavit metodu vracející typ, co nelze do položky přiřadit, jako `RuleFor` pro vyplnění dané položky.

### Přehled podporovaných náhodných entit

#### RandomGenerator.

##### Random.

- Double
- Float
- Decimal
- Int
- Uint
- Short
- Ushort
- Sbyte
- Byte
- Long
- Ulong
- EvenInt
- OddInt
- Bool
- Guid
- DateTime
- Char
- String

##### Char.

- Char
- Digit
- LowerCaseLetter
- UpperCaseLetter
- Letter
- Alphanumeric
- Ascii
- Vowel  -  smazat/make internal -v zavislosti na tom, jak bude implementovano lorem ipsum
- Consonant  smazat/make internal -v zavislosti na tom, jak bude implementovano lorem ipsum
- HexadecimalDigit
- WhiteSpace

##### String.

- String
- LowerCaseLetters
- UpperCaseLetters
- Letters
- Aplhanumeric
- Hexadecimal

##### Enumerable.

- Double
- Float
- Decimal
- Int
- Uint
- Short
- Ushort
- Sbyte
- Byte
- Long
- Ulong
- EvenInt
- OddInt
- Bool
- Guid
- DateTime
- Char
- String

###### RandomCollection

###### RandomList

###### RandomEnumerable



## TODO: 

#### Nullable  varianty základních typů s konfigurovatelnou pravděpodobností výskytu null

- [ ] 

**Priorita: 1**

####  Hexadecimal number formater
- [ ] 

**Priorita: 1**

Umožnit uživateli navolit si formát generovaných hexadecimáních stringů a charů, co reprezentují hexadecimální číslice.

Lower case / upper case hexadecimal digits? Přidat `0x` prefix? 


###  `Pick()` 

**Náhodně vybere jednu z hodnot předaných jako parametry.**

- [ ] 

 **Priorita: 2**
```csharp
public class SomeFaker : BaseFaker<SomeClass>
{
    public SomeFaker()
    {
        RuleFor(c => c.somePrime, rg => rg.Pick(2,3,5,7,13,17,73));
    }
}
```

Jednoduché použití, nevyžaduje, aby uživatel definoval enumerable.

Implementace pomocí: `Pick(param T[] name)`. 

I don't think that **[1]** supports this feature.

##### `Pick(IEnumerable???/ICollection/IList)` 

**Náhodně vybere jednu hodnotu z kolekce.**

```csharp
var databaseKey = setOfValidKeys.PickRandom();
var playerToShoot = rg.Pick(setOfPlayersInRange);
```

Kolekci bude pravděpodobně třeba předat jako parametr konstruktoru fakeru pro uživatelem definovanou třídu.

Nebo `extension` metoda `IEnumerable.PickRandom()` se stejnou funkcionalitou.

### `When()` (podmíněné generování)

- [ ] 

**Priorita: 2**

Umožňuje uživateli stanovit si podmínky, které musí náhodně generované hodnoty splňovat, poskytuje mu lepší kontrolu nad generovaným náhodným obsahem, umožňuje určit logické souvislosti mezi jednotlivými náhodně vyplňovanými položkami (např. konkrétní hodnota v jedné položce implikuje konkrétní hodnotu (či konkrétní způsob vyplnění) jiné položky).

Fluent syntax podobná `linq` spojuje pravidla, co spolu logicky souvisí do jednoho příkazu.  Narozdíl od **[1]** není třeba využívat fluent syntax při psaní pravidel, co nemají logickou souvislost. Taková pravidla mohou stát každé jako samostatný příkaz, čímž nedojde ke slití celého těla konstruktoru do jednoho sáhodlouhého příkazu. Pouze pravidla, u kterých to dává logický smysl, budou tvořit jeden příkaz. 

```csharp
public class PersonFaker : BaseFaker<Person>
{
    public PersonFaker()
    {
         RuleFor(p => p.Gender, random => random.Person.Gender())
         	.When(p => p.Gender == Genders.female)
         	.RuleFor(p => p.Name, random => random.Person.FemaleName())
         	.When(p => p.Gender == Genders.male)
         	.RuleFor(p => p.Name,random => random.Person.MaleName() );
        //else?
    }
}
```
```csharp
public class ColorFaker : BaseFaker<Color>
{
    public ColorFaker()
    {
         RuleFor(c => c.Color, random => random.Pick(col.Red, col.Blue, col.Green).
         When(c => c.Color == col.Green).
         RuleFor(c => c.G, random => 255).
         When(c => c.Color == col.Blue).
         RuleFor(c => c.B, random => 255).
         When(c => c.Color == col.Red).
         RuleFor(c => c.R, random => 255).
    }
}
```

```csharp
public class GalaxyResident
{
	public int Status {get; set;}
	public bool Hitchhiker {get; set;}
}

public class GalaxyResidentFaker : BaseFaker<GalaxyResidentClass>
{
    public GalaxyResidentFaker()
    {
        RuleFor(s => s.Status, rg => rg.Pick(1,2,42,73)).
		When(s => s.Status == 42 ).
		RuleFor(s => s.Hitchhiker, rg=>true );
        
        When(/*conditon*/).RuleFor(/*...*/).RuleFor(/*...*/);
    }
}
```

I don't think that **[1]** supports this feature.

### Lorem Ipsum v různých jazycích

- [ ] 

**Priorita: 2**

Náhodný string který četností výskytu písmen připomíná text v daném jazyce.

Markovovské texty *(4/5)* *Generování náhodného textu napodobujícího text zadaný (řekněme se stejnými pravděpodobnostmi výskytu znaku v závislosti na předchozích k znacích). Případně totéž pro slova. [Lze též řešit způsobem připomínajícím Huffmanovu kompresní metodu, na požádání předvedu.]*  http://mj.ucw.cz/vyuka/zap/

**[1]** supports this in some way.

### Generování náhodných čísel s jinou pravděpodobnostní distribucí než rovnoměrným rozložením.

- [ ] 

### `AutoFaker<T>` typ

**Jednodušší syntax pro vytváření fakerů, které vyplňují položky defaultními randomizačními metodami (bez explicitního specifikování `RuleFor` či`SetFaker`).**

- [ ] 

**Priorita: 3**

`AutoFaker<T>`  je třída poděděné od `BaseFaker<T>`, která má flag `faker.FillEmptyMembers` nastavený automaticky na `UnfilledMembers.DefaultRandomFunc`. Implementačně velmi jednoduché rozšíření API, které zjednoduší a zpříjemní použití fakerů. Díky AutoFakeru lze nadefinovat plně funkční faker na jednom řádku.

Rozmyslet si: jaké další flags souvisí s imlpicitním vyplňováním pomocí defaultních randomizačních funkcí a jak by měly být nastaveny.

```csharp
// without AutoFaker<T> type
public class SimplePersonFaker : BaseFaker<Person>
{
    public SimplePersonFaker()
    {
        this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
    }
}
// after AutoFaker<T> type is implemented
public class EvenSimplerPersonFaker : AutoFaker<Person> { }
```

### StrickMode 

**Vynutí stanoven pravidel pro všechny veřejné položky.**

- [ ] 

**Priorita: 3**

Flag na Fakeru. Je-li nastaven na `true`, všechny veřejné položky třídy musí v konstruktoru nastavené pravidlo pro vyplnění (metodou `RuleFor` či `SetFaker`). Chybějící pravidlo vyvolá `FakerException`. 

Užitečné pro třídy s velkým množstvím veřejných položek v kontextu, kdy je třeba náhodně vyplnit všechny veřejné položky. Umožňuje uživateli zavést kontrolu, že na vyplnění nějaké položky nezapomněl.

**Rozmyslet si:** jak se `StrickMode` chová ve vztahu k `FillEmptyMembers = UnfilledMembers.DefaultRandomFunc`.

```csharp
public class Person
{
    this.StrictMode = true; 		//strict mode
    public Guid Id { get; set; }
    public int Age { get; set; }
    public string Name { get; set; }
    public bool IsAwesome { get; set; }
}
```

Taken from **[1]**.

### `Ignore()` 

 **Má vliv, když `faker.FillEmptyMembers` je nastaven na `UnfilledMembers.DefaultRandomFunc`. Ponechá položku nevyplněnou.**

- [ ] 

**Priorita: 3**

Metoda volaná z konstruktoru custom fakeru (stejně jako `RuleFor`, `SetFaker`). Umožňuje označit položky, které nemají být vyplněny pseudonáhodným obsahem, pokud by faker byl nakonfigurován tak, aby vyplňoval položky bez pravidel voláním defaultní random funkce pro daný typ.  Učiní implicitní vyplňování bez přímého specifikování RuleFor flexibilnější a tím rozšíří množinu případů, kdy lze celý Faker nakonfigurovat jednoduše pomocí pár řádek.

Vhodné pro položky disponující nějakou hodnotou, co nemá být při generování náhodného obsahu přepsána.

```csharp
public class SimplePersonFaker : BaseFaker<Person>
{
    public SimplePersonFaker()
    {
        this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
        RuleFor(p => p.Name, rg => rg.Person.FirstName());
        Ignore(p => p.ProprertyThatShouldNotBeFaked);
    }
}
```

```csharp
public class DatabaseRecordFaker : BaseFaker<DatabseRecord>
{
    public DatabaseRecordFaker()
    {
        this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc; 
        //suppose that records need to have specific keys to simulate some beviour in a database
        Ignore(record => record.key);
        // all other fields just neeed to contain some value, that does not matter that much
    }
}
```

I don't think that **[1]** supports this feature.

### Attribute `[FakerIgnore]`

 **Má vliv, když `faker.FillEmptyMembers` je nastaven na `UnfilledMembers.DefaultRandomFunc`. Ponechá položku nevyplněnou. Pro třídy psané s úmyslem jejich vyplňování náhodným obsahem.** 

- [ ] 

**Priorita: 3**

Atribut přidělovaný položkám uživatelem definované třídy, o které se již při implementaci ví, že bude vyplována pomocí fakeru a tento fakt je důležitou součástí jejího účelu.

Využití atributu `[FakerIgnore]` má podobné důsledky jako zavolání metody `Ignore` v konstruktor fakeru. Položka označená tímto atributem bude ignorována všemi fakery specializovanými na danou třídu. 

Atribut má vliv pouze při implicitním vyplňování defaultními randomizačními metodami (flag  `faker.FillEmptyMembers`  nastavený na `UnfilledMembers.DefaultRandomFunc`). 

`RuleFor` je silnější než `[FakerIgnore]` atribut. Je-li v nějakém konstruktoru fakeru nastaveno `RuleFor` pro položku označenou jako `[FakerIgnore]`, položka bude vyplněna obsahem dle tohoto pravidla bez ohledu na `[FakerIgnore]`.

```csharp
public class Person
{
    public Guid Id { get; set; }
    public int Age { get; set; }
    public string Name { get; set; }
    public bool IsAwesome { get; set; }
    [FakerIgnore]
    public ulong ProprertyThatShouldNotBeFaked {get; set; }
}
```

```csharp
public class DatabeseRecord
{
	[FakerIgnore]
	public Guid Key { get; set; }
	/*
	* Bunch of other members
	*/
}
```

I don't think that **[1]** supports this feature.

### Explicitní konverze z `faker<T>` na `T`

- [ ] 

**Priorita: 4**

Explicitní konverze z fakeru na typ, na který je specializován, co na fakeru volá metodu`Generate`.

Implementačně jednoduché, o užitečnosti lze pochybovat.

```csharp
BasicPersonFaker personFaker = new BasicPersonFaker();
// calls personFaker.Generate();
Person p = (Person) personFaker;
```

Taken from **[1]**.

### Náhodný výběr z `Enum`

- [ ] 

**Priorita: 4**

Funkcionalita se částečně překrývá s `Pick()` metodou.  

### Implementace dalších algoritmů pro generování náhodných čísel, co produkují sekvence náhodných čísel s trochu odlišnými vlastnostmi.

- [ ] 

**Priorita: 5**

Možná trochu overkill v rámci toho, že většina využití fakeru nemá tak striktní požadavky na kvalitu náhodnosti.

### Shuffle

**Zpermutuje položky v poli / v listu.**

- [ ] 

**Priorita: 5**

Taken from **[1]**.

### Replace numbers (##)

**Nahradí ve stringu výskyty # náhodnými číslicemi.**

- [ ] 

**Priorita: 5**

```
"Abraka ## dabra" ~> "Abraka 73 dabra"
```

Taken from **[1]**

### Visual studio extension generating and detecting missing `.RuleFor()` rules 

- [ ] 

**Priority: 6**

Taken from **[1]**. Part of Bogus Premium. Tutorials to create VS extensions available online, generate the text itself based on reflection should not be so demanding.

### Snippet for `RuleFor`

- [ ] 

**Priority: 6**

After typing `RuleFor` and pressing tab twice the skeleton of `RuleFor` function call should appear (as it does for `for` cycle).

Unimportant. 

### Unique, Exclude, Clear

- [ ] 

**Priority: 6**

Taken from **[4]** or **[5]**.

Generate random values without repetition (`Unique`). Set some values as already used so that unique generating method will never produce them (`Exclude`). Clear the cash of already used values (`Clear`).

Exception when no unique value can be generated

Probably more painful to implement than it is worth. How to do this effectively and without damaging the distribution of random values?

### `IValueProvider`

- [ ] 

```csharp
//just some ides, implementation not thought thru deeply enough to provide exact example of the usage
public class SomeFaker : BaseFaker<SomeClass>
{
    public SomeFaker()
    {
        RuleFor(s => s.someValue, rg =>rg.Random.Int(upper:42));
        RuleFor(s => s.anotherValue, s => s.AwesomeMethodToGenerateValuesOfThisMember());
        //IValue interface as a private contract on a user defined type
        RuleFor(s=> s.value, (IValueProvider)k=> k.anotherValuesProvidingMethod);
    }
}
```

To enable to set as a Rule (by `RuleFor` method) for filling particular member (from within a `Generate` method) also methods implemented on some other (for instance user defined) type apart from methods implemented on `RandomGenerator`. 

I don't think that **[1]** supports this feature.

**Neimplementovat**, nedavá příliš smysl, chovaní se dá substituovat buď voláním `Populate()`  instanci již předvyplněnou příslušnými daty a s `Ignore()` nastaveným tak, aby se předvyplněné položky nepřepsaly, či voláním statických metod z `RuleFor()`:

```cs
public class SomeFaker : BaseFaker<SomeClass>
{
    public SomeFaker()
    {
        RuleFor(s => s.someValue, rg =>rg.Random.Int(upper:42));
        RuleFor(s => s.anotherValue, _ => UsersStaticClass.AwesomeStaticMethod());
    }
}
```

### Attribute affecting what is a default random function for a member

**Neat  but possibly impossible. Think thru while studying attributes.**

- [ ] 

```csharp
public class Person
{
    public Guid Id { get; set; }
    public int Age { get; set; }
	
    //attribute
    [Fake(f => f.Person.Name)] //something like this 
    public string Name { get; set; }
    
    public bool IsAwesome { get; set; }
}
```

I'm not really sure how attributes work and how difficult (if even possible) this would be to implement. Suitable for classes that are written with intention to be faked. Idea is to allow user to set default random function (used when `faker.FillEmptyMembers` is set to `UnfilledMembers.DefaultRandomFunc`) while implementing the class in question. 

I don't think that **[1]** supports this feature.

###  Bohaté API, široká škála náhodných entit, co je možné generovat - jen nápady

##### Person

- [ ] Names - Same as in name section
- [ ] PhoneNumber
- [ ] Email
- [ ] Job?
- [ ] Title
- [ ] Address
- [ ] Gender
- [ ] BloodType?

- [ ] `RandomPerson` (Id, FirstName, LastName,Title, Gender, Email, PhoneNumber, DateOfBitrh, Address, Company...) - method generating the whole predefined Person object at once, for other common real world entities as well - for instance Company, Address... Taken from **[1]**. 

##### Address

- [ ] City
- [ ] Street
- [ ] CityAndStreet
- [ ] BuildingNumber
- [ ] CityStreetBuidingNumber
- [ ] Country
- [ ] ZipCode?
- [ ] PostCode
- [ ] TimeZone?
- [ ] FullAddress - similar to `RandomPerson`

##### DateTime

- [ ] DateTime

- [ ] past

- [ ] future

- [ ] recent

- [ ] soon

- [ ] recentOffset  `DateTimOffset` https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset?view=net-5.0  

- [ ] soonOffset  - between now and date passed as argument

- [ ] unixTime

- [ ] month

- [ ] monthAbbreviation

- [ ] weekday

- [ ] weekdayAbbreviation

- [ ] year

- [ ] dayOfMonth

- [ ] TimeSpan

##### Name - Male, Female, Unisex varinats

- [ ] FirstName
- [ ] LastName
- [ ] FullName
- [ ] FullNameWithMiddle
- [ ] Title/NamePreffix
- [ ] Initials

##### Other

- [ ] Emoji
- [ ] Color
- [ ] Direction (North...)
- [ ] ProgrammingLanguage
- [ ] FamilyMember
- [ ] Json
- [ ] environment variable - unix/windows

##### Network

- [ ] Email
- [ ] Email(name) - Email corresponding to name
- [ ] DomainName
- [ ] DomainSuffix
- [ ] Ipv6
- [ ] Ipv4
- [ ] Mac
- [ ] Password
- [ ] Url

##### Git [2]

- [ ] branch
- [ ] commit message 
- [ ] commitHash
- [ ] commitEntry

##### Files

- [ ] FileName
- [ ] DirectoryPathUnix
- [ ] FilePathUnix
- [ ] DirectoryPathWindows
- [ ] FilePathWindows
- [ ] FileType  (.txt .zip .sln .....)

##### Lorem

- [ ] Word
- [ ] WordsArray
- [ ] Words(int) 
- [ ] Sentence
- [ ] Sentences(int)
- [ ] Paraghraph
- [ ] Paragraphs(int)
- [ ] Lines

##### Finance

- [ ] Price
- [ ] CurrencyName
- [ ] CurrencyCode
- [ ] CurrencySymbol?
- [ ] AccountNumber
- [ ] AccountName
- [ ] TransactionType
- [ ] CreditCardNumber
- [ ] BitcoinAddess

##### Random  

- [ ] Hash
- [ ] ArrayElement / List 
- [ ] ArrayElements / List

##### CoronaIpsum? :D

##### Comerce?

##### Company?

##### Database

##### Measurement? **[3]** https://github.com/faker-ruby/faker/blob/master/doc/default/measurement.md

##### Vehicle?

##### Device?

##### Drone? **[3]** 

##### Movie? [3]

##### Song? [3]

##### Book? [3]

- [ ] Author
- [ ] Genre
- [ ] NumPages
- [ ] WholeBook
- [ ] Publisher?
- [ ] PublishedWhen?
- [ ] PublishedWhere?



[1] https://github.com/bchavez/Bogus

[2] https://github.com/marak/Faker.js/

[3] https://github.com/faker-ruby/faker

[4] https://metacpan.org/release/JASONK/Data-Faker-0.07

[5] https://github.com/fzaninotto/Faker

[6] https://github.com/eranpeer/FakeIt

https://github.com/bchavez/Bogus/issues - bogus issues - source of an inspiration