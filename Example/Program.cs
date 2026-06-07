using System.Diagnostics;
using Slangc.NET;

string[] targets = ["hlsl", "glsl", "dxil", "spirv"];

string slang = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Shaders", "Test.slang"));

foreach (string target in targets)
{
    Stopwatch stopwatch = Stopwatch.StartNew();

    args =
    [
        "-profile", "sm_6_6",
        "-matrix-layout-row-major",
        "-entry","VSMain", "-stage", "vertex",
        "-entry","PSMain", "-stage", "pixel",
        "-target", target
    ];

    byte[] shader = SlangCompiler.CompileWithReflection(slang, args, out SlangReflection reflection);

    stopwatch.Stop();

    Console.WriteLine($"Target: {target}");
    Console.WriteLine($"Compilation Time: {stopwatch.ElapsedMilliseconds} ms");
    Console.WriteLine($"Length: {shader.Length} bytes");
    Console.WriteLine($"Reflection Parameters: {reflection.Parameters.Length} items");
    Console.WriteLine($"Reflection EntryPoints: {reflection.EntryPoints.Length} items");
    Console.WriteLine();
}