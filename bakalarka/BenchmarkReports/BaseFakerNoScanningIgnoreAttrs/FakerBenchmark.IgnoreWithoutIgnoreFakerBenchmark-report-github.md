``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.804 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                   Method |     Mean |    Error |   StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------- |---------:|---------:|---------:|-----:|-------:|------:|------:|----------:|
|   ScanningBaseFakerATTRs | 712.1 ns | 12.64 ns | 11.82 ns |    1 | 0.6323 |     - |     - |     992 B |
| ScanningBaseFakerNoATTRs | 724.4 ns | 10.25 ns |  9.58 ns |    1 | 0.6323 |     - |     - |     992 B |
