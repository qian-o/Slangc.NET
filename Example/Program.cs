using Slangc.NET;

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

SlangCompiler.Error += Console.WriteLine;

SlangCompiler.PreprocessorDefines["SRGB_TO_LINEAR"] = "0";

byte[] legacySpv = SlangCompiler.Compile(args, out _);

SlangCompiler.PreprocessorDefines["SRGB_TO_LINEAR"] = "1";

byte[] linearSpv = SlangCompiler.Compile(args, out _);