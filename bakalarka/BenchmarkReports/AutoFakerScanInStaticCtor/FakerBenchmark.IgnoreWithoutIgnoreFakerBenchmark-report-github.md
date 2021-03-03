``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.804 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                     Method |        Mean |     Error |    StdDev |      Median | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------- |------------:|----------:|----------:|------------:|-----:|-------:|------:|------:|----------:|
|      BaseFakerNoAttributes |    843.8 ns |   8.44 ns |   6.59 ns |    845.1 ns |    1 | 0.7906 |     - |     - |   1.21 KB |
|        BaseFakerAttributes |    933.9 ns |  25.13 ns |  73.32 ns |    911.7 ns |    2 | 0.7906 |     - |     - |   1.21 KB |
| BaseFakerAttributesRuleFor |  2,576.3 ns |  45.87 ns |  42.90 ns |  2,566.0 ns |    3 | 1.2741 |     - |     - |   1.95 KB |
|        AutoFakerAttributes |  4,028.8 ns |  79.55 ns |  85.12 ns |  3,987.2 ns |    4 | 1.7700 |     - |     - |   2.71 KB |
| AutoFakerAttributesRuleFor |  7,216.8 ns | 180.40 ns | 523.38 ns |  7,060.4 ns |    5 | 2.1820 |     - |     - |   3.34 KB |
|      AutoFakerNoAttributes | 16,987.2 ns | 308.49 ns | 442.43 ns | 16,851.5 ns |    6 | 8.3618 |     - |     - |  12.81 KB |
