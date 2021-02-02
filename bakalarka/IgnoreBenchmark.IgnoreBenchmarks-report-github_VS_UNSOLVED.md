``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.746 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.101
  [Host]     : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT


```
|              Method |        Mean |     Error |    StdDev | Ratio | RatioSD | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |------------:|----------:|----------:|------:|--------:|-----:|-------:|------:|------:|----------:|
| WithoutIgnoreNOScan |    253.3 ns |   4.82 ns |   8.31 ns |  0.01 |    0.00 |    1 | 0.4282 |     - |     - |     672 B |
|   WithoutIgnoreScan | 21,394.1 ns | 301.76 ns | 267.51 ns |  0.99 |    0.02 |    2 | 2.9297 |     - |     - |    4617 B |
|      WithIgnoreScan | 21,605.3 ns | 358.42 ns | 335.27 ns |  1.00 |    0.00 |    2 | 2.9297 |     - |     - |    4617 B |
