``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.804 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|        Method |       Mean |     Error |    StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |-----------:|----------:|----------:|-----:|-------:|------:|------:|----------:|
|     OneMember |   259.3 ns |   4.97 ns |   4.65 ns |    1 | 0.1173 |     - |     - |     184 B |
|    TwoMembers |   437.2 ns |   6.23 ns |   5.82 ns |    2 | 0.1631 |     - |     - |     256 B |
|        Nested | 1,249.8 ns |  23.13 ns |  22.72 ns |    3 | 0.2899 |     - |     - |     456 B |
| LotOfMemebers | 6,924.8 ns | 134.03 ns | 125.37 ns |    4 | 3.3951 |     - |     - |    5336 B |
