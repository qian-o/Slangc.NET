using System.Diagnostics;
using Slangc.NET;

Stopwatch stopwatch = Stopwatch.StartNew();

args =
[
    Path.Combine(AppContext.BaseDirectory, "Shaders", "Test.slang"),
    "-matrix-layout-row-major",
    "-target", "spirv",
    "-profile", "spirv_1_5",
    "-fvk-b-shift", "0", "all",
    "-fvk-t-shift", "20", "all",
    "-fvk-s-shift", "40", "all"
];

byte[] spv = SlangCompiler.CompileWithReflection(args, out SlangReflection reflection);

stopwatch.Stop();

Console.WriteLine($"Compilation Time: {stopwatch.ElapsedMilliseconds} ms");
Console.WriteLine($"SPIR-V: {spv.Length} bytes");
Console.WriteLine($"Reflection JSON: {reflection.Json}");