``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.804 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                      Method |     Mean |   Error |  StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------- |---------:|--------:|--------:|-----:|-------:|------:|------:|----------:|
|    RuleForGenerateOneMember | 298.7 ns | 4.98 ns | 4.66 ns |    1 | 0.1173 |     - |     - |     184 B |
| SetRuleForGenerateOneMember | 304.8 ns | 6.13 ns | 6.02 ns |    1 | 0.1173 |     - |     - |     184 B |
| ForSetRuleGenerateOneMember | 316.3 ns | 6.26 ns | 7.45 ns |    2 | 0.1173 |     - |     - |     184 B |
