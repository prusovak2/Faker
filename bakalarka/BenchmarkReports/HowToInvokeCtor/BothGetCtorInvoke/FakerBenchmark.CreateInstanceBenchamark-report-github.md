``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.804 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|            Method |      Mean |     Error |    StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------ |----------:|----------:|----------:|-----:|-------:|------:|------:|----------:|
|          Populate |  70.45 ns |  1.397 ns |  1.238 ns |    1 | 0.0867 |     - |     - |     136 B |
| GenerateParamless | 255.60 ns |  4.752 ns |  6.344 ns |    2 | 0.0858 |     - |     - |     136 B |
|     GenerateParam | 527.40 ns | 10.365 ns | 11.090 ns |    3 | 0.2241 |     - |     - |     352 B |
