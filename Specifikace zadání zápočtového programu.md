# Specifikace zadání zápočtového programu

### Za předměty Jazyk C# a platforma .NET (NPRG035) a Pokročilé programování pro .NET I (NPRG038)

Mým cílem je naimplementovat generátor pseudonáhodného obsahu objektů (Faker), tedy software, co bude schopen vygenerovat (případně velké) množství instancí uživatelem naimplementovaných tříd tak, že výsledné instance budou obsahovat data, která se jeví náhodná, ale jsou relevantní v rámci uživatelem definovaných podmínek.

### Shrnutí projektu:

Projekt by měl obsahovat dva myšlenkové funkční celky.

Prvním je vlastní implementace generátoru pseudonáhodných čísel. Tzn. v projektu nebude využita třída `System.Random`, místo ní bude naimplementována vlastní třída `RandomGenerator`, která bude poskytovat API pro generování všech základních předdefinovaných datových typů v C# (`int`, `long`, `byte`, `double`, `decimal`, `bool`,` char`...), jakožto i zajímavějších typů dat. Jako příklad mě napadá náhodná adresa, jméno, řetězec alfanumerických znaků, GUID, string, co reprezentuje náhodné hexadecimální číslo, další variace na téma náhodný string/text, sudé číslo, liché číslo...

Základem třídy `RandomGenerator` bude implementace nějakého existující algoritmu pro generování pseudonáhodných čísel (pravděpodobně `Xoshiro256**`). Na rozdíl od `System.Random` nebudou dvě hned po sobě vytvořené instance `RandomGeneratoru` produkovat shodné sekvence náhodných čísel.

Implementace využívá extension metody, probírané v předmětu NPRG038.

Druhou částí projektu je vytvoření API, které zprostředkuje funkce `RandomGeneratoru` pro vyplňování objektů pseudonáhodným obsahem. Toto API by mělo mít jednoduché a intuitivní použití. Uživatel by měl napsat jednoduchou třídu, ve které určí, jakým způsobem mají být položky jeho "datonosné" třídy vyplněny (volání metody `RuleFor()`). Pro položky složitějšího typu bude možné předat již vytvořený Faker pro daný typ a využít ho k vyplnění položky (`SetFaker()`).

```cs
public class Storage
{
    public ValueClass Value { get; set; }

    public int Test { get; set; }
}
//nástřel použití
public class StorageFaker : BaseFaker<Storage>
 {
     public StorageFaker()
     {
         RuleFor(e => e.Test, f => f.Int());
         SetFaker(e => e.Value, new BaseFaker<ValueClass>());         
     }
 }
```

### Technické zajímavosti projektu:

Implementace bude využívat generické typy, lambda funkce, delegáty a především reflection.

Narozdíl od `AutoBugu` (existující implementace podobného, dostupná jako NuGet package), nebude využita fluent syntax pro nastavování pravidel pro nesouvisející položky (tzn. jedno `RuleFor` bude jeden příkaz), což umožní program krokovat. Navíc bude věnováno víc prostoru generování náhodných kolekcí a uživatel bude mít možnost nastavit vlastní seed pro náhodný generátor a duplikovat tak již nastalou situaci (vygenerovat stejný obsah objektu, který například v jeho programu způsobil výjimku, tuto situaci nadále zkoumat, debugovat).

### K čemu je to dobré a proč jsem si to vybrala:

Faker je využitelný pro testování softwaru, debugování a pro vytváření benchmarks.

Na projektu se chci především naučit používat reflection. Dále mě láká matematický přesah a příležitost nastudovat si něco o generátorech pseudonáhodných čísel.
