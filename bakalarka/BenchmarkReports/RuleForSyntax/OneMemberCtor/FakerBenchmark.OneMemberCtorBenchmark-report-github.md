``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.804 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                  Method |     Mean |     Error |    StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------ |---------:|----------:|----------:|-----:|-------:|------:|------:|----------:|
| SetRuleForCtorOneMember | 1.705 μs | 0.0322 μs | 0.0358 μs |    1 | 1.1005 |     - |     - |   1.69 KB |
|    RuleForCtorOneMember | 1.714 μs | 0.0300 μs | 0.0234 μs |    1 | 1.0853 |     - |     - |   1.66 KB |
| ForSetRuleCtorOneMember | 2.022 μs | 0.0400 μs | 0.0679 μs |    2 | 1.1520 |     - |     - |   1.77 KB |
