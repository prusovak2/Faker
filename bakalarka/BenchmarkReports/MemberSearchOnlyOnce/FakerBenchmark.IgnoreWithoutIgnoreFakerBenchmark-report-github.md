``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.804 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                   Method |     Mean |    Error |   StdDev | Rank |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------- |---------:|---------:|---------:|-----:|--------:|------:|------:|----------:|
|   ScanningBaseFakerATTRs | 21.10 μs | 0.420 μs | 0.561 μs |    1 |  3.9978 |     - |     - |   6.14 KB |
| ScanningBaseFakerNoATTRs | 26.49 μs | 0.522 μs | 0.828 μs |    2 | 10.1318 |     - |     - |  15.53 KB |
|   RuleForBetweenGenerate | 32.14 μs | 0.634 μs | 1.295 μs |    3 | 10.5591 |     - |     - |  16.24 KB |
