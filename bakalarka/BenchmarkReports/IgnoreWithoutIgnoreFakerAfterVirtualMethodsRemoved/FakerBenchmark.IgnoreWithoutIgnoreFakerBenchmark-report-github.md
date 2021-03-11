``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.804 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                     Method |        Mean |     Error |    StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------- |------------:|----------:|----------:|-----:|-------:|------:|------:|----------:|
|        BaseFakerAttributes |    681.5 ns |  13.25 ns |  14.17 ns |    1 | 0.8469 |     - |     - |    1.3 KB |
|      BaseFakerNoAttributes |    682.4 ns |  13.54 ns |  18.54 ns |    1 | 0.8469 |     - |     - |    1.3 KB |
| BaseFakerAttributesRuleFor |  2,089.9 ns |  40.59 ns |  45.12 ns |    2 | 1.3809 |     - |     - |   2.12 KB |
|        AutoFakerAttributes |  4,082.7 ns |  80.82 ns | 105.09 ns |    3 | 1.8158 |     - |     - |   2.79 KB |
| AutoFakerAttributesRuleFor |  5,907.5 ns | 113.96 ns | 139.95 ns |    4 | 2.3117 |     - |     - |   3.55 KB |
|      AutoFakerNoAttributes | 17,088.3 ns | 324.92 ns | 303.93 ns |    5 | 8.4229 |     - |     - |  12.93 KB |
