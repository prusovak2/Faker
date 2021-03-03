``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.804 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|        Method |       Mean |     Error |    StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |-----------:|----------:|----------:|-----:|-------:|------:|------:|----------:|
|     OneMember |   215.7 ns |   3.99 ns |   3.74 ns |    1 | 0.1018 |     - |     - |     160 B |
|    TwoMembers |   374.4 ns |   5.10 ns |   4.78 ns |    2 | 0.1326 |     - |     - |     208 B |
|        Nested | 1,032.6 ns |  20.69 ns |  20.32 ns |    3 | 0.2594 |     - |     - |     408 B |
| LotOfMemebers | 6,612.0 ns | 116.12 ns | 108.62 ns |    4 | 3.2959 |     - |     - |    5180 B |
