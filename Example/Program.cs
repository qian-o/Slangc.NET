using System.Diagnostics;
using Slangc.NET;

string[] targetArgs = ["hlsl", "glsl", "dxil", "spirv"];

foreach (string target in targetArgs)
{
    Stopwatch stopwatch = Stopwatch.StartNew();

    args =
    [
        Path.Combine(AppContext.BaseDirectory, "Shaders", "Test.slang"),
        "-profile", "sm_6_6",
        "-matrix-layout-row-major",
        "-entry","VSMain", "-stage", "vertex",
        "-entry","PSMain", "-stage", "pixel",
        "-target", target
    ];

    byte[] shader = SlangCompiler.CompileWithReflection(args, out SlangReflection reflection);

    stopwatch.Stop();

    Console.WriteLine($"Target: {target}");
    Console.WriteLine($"Compilation Time: {stopwatch.ElapsedMilliseconds} ms");
    Console.WriteLine($"Length: {shader.Length} bytes");
    Console.WriteLine($"Reflection Parameters: {reflection.Parameters.Length} items");
    Console.WriteLine($"Reflection EntryPoints: {reflection.EntryPoints.Length} items");
    Console.WriteLine();
}