```

BenchmarkDotNet v0.13.12, Arch Linux
AMD Ryzen 7 3700X, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.111
  [Host]     : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2


```
| Method             | Mean      | Error    | StdDev   | Gen0   | Allocated |
|------------------- |----------:|---------:|---------:|-------:|----------:|
| ComputeIngredients |  65.44 ns | 0.375 ns | 0.350 ns | 0.0257 |     216 B |
| DoughComponents    | 143.66 ns | 0.971 ns | 0.908 ns | 0.0391 |     328 B |
