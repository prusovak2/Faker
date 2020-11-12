# Features to implement

##  Rich API, huge variety of random entities to generate - just ideas

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
- [ ] FullAddress

##### Comerce?
##### Company?
##### Database
##### DateTime

- [ ] DateTime

- [ ] past

- [ ] future

- [ ] recent

- [ ] soon

- [ ] recentOffset  

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
- [ ]  environment variable - unix/windows
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
##### Git **[2]**
- [ ] branch
- [ ] commit message 
- [ ] commitHash
- [ ] commitEntry
##### Random  

- [ ] Hash
- [ ] ArrayElement / List 
- [ ] ArrayElements / List

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

##### Person

- [ ] Names - Same as in name section
- [ ] PhoneNumber
- [ ] Email
- [ ] Job?
- [ ] Title
- [ ] Address
- [ ] Gender
- [ ] BloodType?

##### CoronaIpsum? :D

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
- [ ]  PublishedWhere?


#### Nullable  variants of a basic type with configurable probability of a null

- [ ] 


###  `Pick` method 

- [ ] 

```csharp
public class SomeFaker : BaseFaker<SomeClass>
{
    public SomeFaker()
    {
        RuleFor(s => s.someValue, rg => rg.Pick(1,2,42,73));
    }
}
```

Randomly chooses one from passed values. Convenient to use, without requiring a user to declare an enumerable himself. 

Implementation something like `Pick(param T[])`.  

I don't think that **[1]** supports this feature.

### `When` (conditioned generation)

- [ ] 
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

To provide user with a more complex control over the generated random content and to allow him to state some conditions that the generated values must abide. Or to declare some relations between class members, whose values do have some logical link (for instance value of one member determines the value of the other).

Fluent syntax resembling `linq` used to connect rules that are logically linked together. No need to use fluent syntax for unrelated rules  - no need to write the whole `ctor` as one long command.  

I don't think that **[1]** supports this feature.

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

### `Ignore` method (at the same level as `RuleFor`)

- [ ]   

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

To allow to leave some members blank when `faker.FillEmptyMembers` is set to `UnfilledMembers.DefaultRandomFunc`. To make implicit filling without specifying `ruleFor` particular properties more flexible and easier to use and subsequently make faker configuration as easy and convenient as possible.

I don't think that **[1]** supports this feature.

### Lorem Ipsum in multiple languages

- [ ] 

Random string that resembles a text in existing languages. (Approach to generating similar to Huffman coding?)

Markovovské texty *(4/5)* *Generování náhodného textu napodobujícího text zadaný (řekněme se stejnými pravděpodobnostmi výskytu znaku v závislosti na předchozích k znacích). Případně totéž pro slova. [Lze též řešit způsobem připomínajícím Huffmanovu kompresní metodu, na požádání předvedu.]*  http://mj.ucw.cz/vyuka/zap/

**[1]** supports this in some way.

### Random selection from `Enum`

- [ ] 

### StrickMode 

- [ ] 

```csharp
public class Person
{
    this.StrictMode=true;
    public Guid Id { get; set; }
    public int Age { get; set; }
    public string Name { get; set; }
    public bool IsAwesome { get; set; }
}
```

Taken from **[1]**.

When this flag is set, Faker checks whether all members of a class it fakes do have a `RuleFor` set for them. Faker will throw a `FakerException` otherwise. 

### Attribute `[FakerIgnore]`

- [ ] 

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

Similar functionality as `Ignore` method. Suitable for classes that are written with intention to be faked. While implementing his class user can add `[FakerIgnore]` attribute to some class members which causes this members to be ignored by all fakers specialized on this class. `[FakerIgnore]` attribute only affects implicit faking with `faker.FillEmptyMembers`  set to `UnfilledMembers.DefaultRandomFunc`. `RuleFor` is considered more important than `[FakerIgnore]` attribute.  When `RuleFor` is set for member with `[FakerIgnore]` attribute in a `ctor` of some faker, this `RuleFor` is gonna be applied no matter the attribute.

I don't think that **[1]** supports this feature.

### Attribute affecting what is a default random function for a member
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

#### Heuristics to guess the best default random function based on a member name

When the field is named for instance `name`, `person.FirstName` would be used as a default random function. Some heuristics based on substrings of member name. 

I'm not sure whether it's worth it to implement this feature. 

### `AutoFaker<T>` type

- [ ] 

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

`AutoFaker<T>` type inherits from Faker<T> and it has a `faker.FillEmptyMembers`  flag (and some other flags and settings related to implicit faking?) set to `UnfilledMembers.DefaultRandomFunc`. To enrich the API and make it super simple to create basic `Faker` for a type. 

### Explicit conversion from `faker<T>` to `T`

- [ ] 

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
....
BasicPersonFaker personFaker = new BasicPersonFaker();
// calls personFaker.Generate();
Person p = (Person) personFaker;
```

Definitely to implement.

Taken from **[1]**.

### `DateTime` formater, Hexadecimal number formater

- [ ] 

Definitely to implement. Bigger variety of `DateTime` formats and formats of hexadecimal numbers is a necessity. 

`DateTime` offset.

### Visual studio extension generating and detecting missing `.RuleFor()` rules 

- [ ] 

Taken from **[1]**. Part of Bogus Premium. Tutorials to create VS extensions available online, generate the text itself based on reflection should not be so demanding.

#### Snippet for `RuleFor`

- [ ] 

After typing `RuleFor` and pressing tab twice the skeleton of `RuleFor` function call should appear (as it does for `for` cycle).

Unimportant. 

#### Unique, Exclude, Clear

- [ ] 

Taken from **[4]** or **[5]**.

Generate random values without repetition (`Unique`). Set same values as already used so that unique generating method will never produce them (`Exclude`). Clear the cash of already used values (`Clear`).

Exception when no unique value can be generated

Probably more painful to implement than it is worth. How to do this effectively and without damaging the distribution of random values?

**More interesting may be picking a random values from array, list without repetition /  permutating items in an array/list randomly**

#### Shuffle, Replace numbers (##), Replace digits (??)

- [ ] 

Taken from **[1]**.





[1] https://github.com/bchavez/Bogus

[2] https://github.com/marak/Faker.js/

[3] https://github.com/faker-ruby/faker

[4] https://metacpan.org/release/JASONK/Data-Faker-0.07

[5] https://github.com/fzaninotto/Faker

[6] https://github.com/eranpeer/FakeIt

https://github.com/bchavez/Bogus/issues