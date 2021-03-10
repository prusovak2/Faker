``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.804 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                     Method |     Mean |    Error |   StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------- |---------:|---------:|---------:|-----:|-------:|------:|------:|----------:|
|    RuleForCtorLotOfMembers | 10.23 μs | 0.182 μs | 0.152 μs |    1 | 4.1351 |     - |     - |   6.35 KB |
| SetRuleForCtorLotOfMembers | 10.90 μs | 0.214 μs | 0.352 μs |    2 | 4.2572 |     - |     - |   6.54 KB |
| ForSetRuleCtorLotOfMembers | 12.54 μs | 0.205 μs | 0.182 μs |    3 | 4.6997 |     - |     - |    7.2 KB |
