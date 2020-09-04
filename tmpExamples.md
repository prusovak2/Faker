```csharp
RandomGenerator rg = new RandomGenerator();
Console.WriteLine("AllRange");
int randomInt = rg.Random.Int();
Console.WriteLine(randomInt);
Console.WriteLine("Less than 42");
randomInt = rg.Random.Int(upper: 42);
Console.WriteLine(randomInt);
Console.WriteLine("Greater than 42");
randomInt = rg.Random.Int(lower: 42);
Console.WriteLine(randomInt);
Console.WriteLine("between 42 and 73");
randomInt = rg.Random.Int(42, 73);
Console.WriteLine(randomInt);
Console.WriteLine("Odd between 42 and 73");
randomInt = rg.Random.IntOdd(42, 73);
Console.WriteLine(randomInt);
```

```csharp
IntOverloadsExample
AllRange
-709368722
Less than 42
-602424976
Greater than 42
1159856799
between 42 and 73
54
Odd between 42 and 73
45
```

```csharp
Console.WriteLine("Infinite Enumerable of bytes");
IEnumerable<byte> bytes = random.Enumerable.Byte();
Console.WriteLine("Enumerable of 3 bytes from [0,42] interval");
bytes = random.Enumerable.Byte(3, 0, 42);
Console.WriteLine("Enumerable of at most 10 bytes");
bytes = random.Enumerable.Byte(10, precise: false);
```

```csharp
Infinite Enumerable of bytes
123
166
174
125
71
Enumerable of 3 bytes from [0,42] interval
13
29
31
Enumerable of at most 10 bytes
24
14
247
157
13
138
156
170
```

```csharp
RandomGenerator random = new RandomGenerator();
string randomString = random.String.Letters();
Console.WriteLine($"Letters: {randomString}");
randomString = random.String.AlphaNumeric(20);
Console.WriteLine($"Alphanumeric: {randomString}");
randomString = random.String.HexadecimalString(1,100);
Console.WriteLine($"Hexadecimal: 0x{randomString}");
```

```csharp
Letters: DXCICGIbvCZVUUCWrJgpKEZGVtdMFcavjSXzyaMfVihMRruMsjLbVbDJInxoZWHTUojiNrNbOHDKPvxGYaFyXxiCUAxdVKgogYATDpSNzIMnKoIJKOJtkjePTuZcfooahHKUlmZMOuWyiTWSdMgiBuGPeNURQwYQGuWeFFyXBvtiTGIUbDGOQoYKkOPWNNiaVsXKUIwnZhxPcCXJlmEEBmkQtaaiFFvPDcMAFSihDyADCZmuaCaUuEoSWSTfwiv
Alphanumeric: QYNyql3i0W5TYCwkpMwk
Hexadecimal: 0x5B
```

