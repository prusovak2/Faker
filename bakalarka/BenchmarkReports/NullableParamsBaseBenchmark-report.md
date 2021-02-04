``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.746 (2004/?/20H1)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.101
  [Host]     : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT


```
|                   Method |      Mean |    Error |   StdDev |    Median | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------- |----------:|---------:|---------:|----------:|-----:|------:|------:|------:|----------:|
|          DoubleParamless |  13.31 ns | 0.228 ns | 0.190 ns |  13.30 ns |    1 |     - |     - |     - |         - |
|  DoubleNullableParamless |  13.49 ns | 0.232 ns | 0.217 ns |  13.48 ns |    1 |     - |     - |     - |         - |
|          DoubleTwoParams |  16.57 ns | 0.233 ns | 0.195 ns |  16.48 ns |    2 |     - |     - |     - |         - |
|           DoubleOneParam |  17.41 ns | 0.275 ns | 0.257 ns |  17.37 ns |    3 |     - |     - |     - |         - |
|             LongOneParam |  18.84 ns | 0.297 ns | 0.278 ns |  18.84 ns |    4 |     - |     - |     - |         - |
|            LongTwoParams |  19.01 ns | 0.268 ns | 0.250 ns |  19.08 ns |    4 |     - |     - |     - |         - |
|    LongNullableTwoParams |  19.10 ns | 0.343 ns | 0.304 ns |  19.10 ns |    4 |     - |     - |     - |         - |
|    LongNullableParamless |  19.46 ns | 0.291 ns | 0.258 ns |  19.41 ns |    4 |     - |     - |     - |         - |
|             IntParamless |  19.98 ns | 0.397 ns | 0.516 ns |  19.97 ns |    5 |     - |     - |     - |         - |
|            LongParamless |  20.48 ns | 0.451 ns | 1.037 ns |  19.93 ns |    5 |     - |     - |     - |         - |
|  DoubleNullableTwoParams |  20.49 ns | 0.172 ns | 0.153 ns |  20.48 ns |    5 |     - |     - |     - |         - |
|     LongNullableOneParam |  20.88 ns | 0.771 ns | 2.260 ns |  20.29 ns |    5 |     - |     - |     - |         - |
|   DoubleNullableOneParam |  20.93 ns | 0.404 ns | 0.358 ns |  20.99 ns |    5 |     - |     - |     - |         - |
|              IntOneParam |  22.33 ns | 0.477 ns | 0.860 ns |  22.42 ns |    6 |     - |     - |     - |         - |
|             IntTwoParams |  22.66 ns | 0.478 ns | 0.862 ns |  22.83 ns |    6 |     - |     - |     - |         - |
|      IntNullableOneParam |  25.13 ns | 0.291 ns | 0.243 ns |  25.14 ns |    7 |     - |     - |     - |         - |
|     IntNullableParamless |  25.14 ns | 0.461 ns | 0.431 ns |  25.04 ns |    7 |     - |     - |     - |         - |
|     IntNullableTwoParams |  25.15 ns | 0.221 ns | 0.184 ns |  25.15 ns |    7 |     - |     - |     - |         - |
|         DecimalParamless | 114.36 ns | 1.269 ns | 1.125 ns | 114.39 ns |    8 |     - |     - |     - |         - |
| DecimalNullableParamless | 115.65 ns | 2.054 ns | 1.821 ns | 115.64 ns |    8 |     - |     - |     - |         - |
|          DecimalOneParam | 265.58 ns | 3.408 ns | 3.188 ns | 264.75 ns |    9 |     - |     - |     - |         - |
|         DecimalTwoParams | 305.35 ns | 5.888 ns | 5.783 ns | 303.35 ns |   10 |     - |     - |     - |         - |
| DecimalNullableTwoParams | 328.81 ns | 2.720 ns | 2.272 ns | 329.04 ns |   11 |     - |     - |     - |         - |
|  DecimalNullableOneParam | 386.67 ns | 6.270 ns | 5.558 ns | 387.19 ns |   12 |     - |     - |     - |         - |
