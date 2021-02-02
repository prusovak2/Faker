``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.746 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.101
  [Host]     : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT


```
|                   Method |        Mean |     Error |    StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------- |------------:|----------:|----------:|-----:|-------:|------:|------:|----------:|
|   WithoutIgnoreBaseFaker |    251.7 ns |   5.03 ns |  11.36 ns |    1 | 0.4282 |     - |     - |     672 B |
|      WithIgnoreBaseFaker |    252.6 ns |   4.97 ns |  10.37 ns |    1 | 0.4282 |     - |     - |     672 B |
| WithoutIgnoreIgnoreFaker | 21,391.5 ns | 409.87 ns | 739.09 ns |    2 | 2.9297 |     - |     - |    4617 B |
|    WithIgnoreIgnoreFaker | 22,056.8 ns | 428.68 ns | 641.63 ns |    3 | 2.9297 |     - |     - |    4617 B |
