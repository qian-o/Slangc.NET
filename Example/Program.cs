using System.Diagnostics;
using Slangc.NET;

Stopwatch stopwatch = Stopwatch.StartNew();

// Whether to enable deserialization of SlangReflection's Json.
SlangCompiler.EnableDeserialization = true;

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

if (SlangCompiler.EnableDeserialization)
{
    Console.WriteLine($"Reflection Parameters: {reflection.Parameters.Length} items");
    Console.WriteLine($"Reflection EntryPoints: {reflection.EntryPoints.Length} items");
}