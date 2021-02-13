``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.804 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                   Method |     Mean |    Error |   StdDev | Rank |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------- |---------:|---------:|---------:|-----:|--------:|------:|------:|----------:|
| ScanningBaseFakerNoATTRs | 33.44 μs | 0.644 μs | 1.301 μs |    1 | 11.8408 |     - |     - |   18.2 KB |
|   ScanningBaseFakerATTRs | 35.15 μs | 0.693 μs | 1.267 μs |    2 |  6.1035 |     - |     - |   9.35 KB |
|   RuleForBetweenGenerate | 36.51 μs | 0.715 μs | 1.307 μs |    3 | 12.3291 |     - |     - |  18.89 KB |
