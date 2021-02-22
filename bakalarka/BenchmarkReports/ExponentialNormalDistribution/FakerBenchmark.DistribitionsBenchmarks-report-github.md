``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.804 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                    Method |     Mean |    Error |   StdDev | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------- |---------:|---------:|---------:|-----:|------:|------:|------:|----------:|
|       ExponentialZiggurat | 23.51 ns | 0.493 ns | 0.658 ns |    1 |     - |     - |     - |         - |
| ExponentialStrightforward | 23.76 ns | 0.333 ns | 0.278 ns |    1 |     - |     - |     - |         - |
|            NormalZiggurat | 24.61 ns | 0.512 ns | 0.454 ns |    2 |     - |     - |     - |         - |
