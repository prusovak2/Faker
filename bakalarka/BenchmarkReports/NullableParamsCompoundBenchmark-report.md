``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.746 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.101
  [Host]     : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT


```
|                  Method |      Mean |     Error |    StdDev | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------ |----------:|----------:|----------:|-----:|------:|------:|------:|----------:|
|          DoubleCompound |  48.21 ns |  0.526 ns |  0.466 ns |    1 |     - |     - |     - |         - |
|  DoubleNullableCompound |  56.72 ns |  0.614 ns |  0.574 ns |    2 |     - |     - |     - |         - |
|             IntCompound |  58.09 ns |  1.059 ns |  0.938 ns |    3 |     - |     - |     - |         - |
|            LongCompound |  60.14 ns |  0.843 ns |  0.704 ns |    4 |     - |     - |     - |         - |
|    LongNullableCompound |  61.78 ns |  1.257 ns |  1.802 ns |    4 |     - |     - |     - |         - |
|     IntNullableCompound |  77.97 ns |  0.783 ns |  0.733 ns |    5 |     - |     - |     - |         - |
|         DecimalCompound | 702.28 ns |  5.937 ns |  5.553 ns |    6 |     - |     - |     - |         - |
| DecimalNullableCompound | 848.07 ns | 11.807 ns | 11.044 ns |    7 |     - |     - |     - |         - |
