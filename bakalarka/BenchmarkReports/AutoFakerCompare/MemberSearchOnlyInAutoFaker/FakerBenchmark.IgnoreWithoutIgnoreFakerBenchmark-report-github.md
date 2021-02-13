``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.804 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                     Method |        Mean |     Error |    StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------- |------------:|----------:|----------:|-----:|-------:|------:|------:|----------:|
|      BaseFakerNoAttributes |    690.6 ns |   5.86 ns |   5.20 ns |    1 | 0.6371 |     - |     - |    1000 B |
|        BaseFakerAttributes |    710.2 ns |  13.93 ns |  13.03 ns |    2 | 0.6371 |     - |     - |    1000 B |
| BaseFakerAttributesRuleFor |  2,243.2 ns |  40.07 ns |  37.48 ns |    3 | 1.1215 |     - |     - |    1760 B |
|        AutoFakerAttributes | 16,403.1 ns | 312.14 ns | 306.56 ns |    4 | 3.5400 |     - |     - |    5554 B |
| AutoFakerAttributesRuleFor | 19,819.9 ns | 308.68 ns | 288.74 ns |    5 | 3.9368 |     - |     - |    6203 B |
|      AutoFakerNoAttributes | 21,823.6 ns | 427.07 ns | 555.31 ns |    6 | 9.6436 |     - |     - |   15165 B |
