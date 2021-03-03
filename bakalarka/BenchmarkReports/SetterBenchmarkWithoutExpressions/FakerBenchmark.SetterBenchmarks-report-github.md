``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.804 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|        Method |       Mean |     Error |    StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |-----------:|----------:|----------:|-----:|-------:|------:|------:|----------:|
|     OneMember |   434.8 ns |   8.30 ns |   8.16 ns |    1 | 0.1426 |     - |     - |     224 B |
|    TwoMembers |   600.4 ns |  11.49 ns |  10.75 ns |    2 | 0.1726 |     - |     - |     272 B |
|        Nested | 1,446.4 ns |  28.35 ns |  30.33 ns |    3 | 0.2995 |     - |     - |     472 B |
| LotOfMemebers | 7,295.1 ns | 116.49 ns | 103.26 ns |    4 | 3.4256 |     - |     - |    5378 B |
