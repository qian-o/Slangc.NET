using System.Diagnostics;
using Slangc.NET;

Stopwatch stopwatch = Stopwatch.StartNew();

args =
[
    Path.Combine(AppContext.BaseDirectory, "Shaders", "Test.slang"),
    "-profile", "sm_6_6",
    "-matrix-layout-row-major",
    "-target", "spirv"
];

byte[] spv = SlangCompiler.CompileWithReflection(args, out SlangReflection reflection);

stopwatch.Stop();

Console.WriteLine($"Compilation Time: {stopwatch.ElapsedMilliseconds} ms");
Console.WriteLine($"SPIR-V: {spv.Length} bytes");
Console.WriteLine($"Reflection JSON: {reflection.Json}");