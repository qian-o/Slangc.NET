using Slangc.NET;

// Currently, I am not adding the slangc native library in Slangc.NET yet, you can get it by installing Vulkan SDK.
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

byte[] vsNotSrgbToLinear = SlangCompiler.Compile(args, out _);

SlangCompiler.PreprocessorDefines["SRGB_TO_LINEAR"] = "1";

byte[] vsSrgbToLinear = SlangCompiler.Compile(args, out _);