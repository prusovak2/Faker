``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.804 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                         Method |     Mean |     Error |    StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------- |---------:|----------:|----------:|-----:|-------:|------:|------:|----------:|
|    RuleForGenerateLotOfMembers | 6.804 μs | 0.1320 μs | 0.1621 μs |    1 | 3.3035 |     - |     - |   5.07 KB |
| SetRuleForGenerateLotOfMembers | 7.705 μs | 0.1316 μs | 0.1167 μs |    2 | 3.3112 |     - |     - |   5.08 KB |
| ForSetRuleGenerateLotOfMembers | 7.981 μs | 0.1594 μs | 0.2016 μs |    3 | 3.3112 |     - |     - |   5.08 KB |
