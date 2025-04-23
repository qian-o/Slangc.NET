using System.Runtime.InteropServices;

namespace Slangc.NET;

public static unsafe class SlangCompiler
{
    private static readonly SlangSession session = new();

    public static event Action<string>? Error;

    public static List<string> SearchPaths { get; } = [];

    public static Dictionary<string, string> PreprocessorDefines { get; } = [];

    public static byte[] Compile(string[] args, out string reflectionJson)
    {
        using SlangCompileRequest request = session.CreateCompileRequest();

        request.SetDiagnosticCallback(DiagnosticCallback, null);

        foreach (string path in SearchPaths)
        {
            request.AddSearchPath(path);
        }

        foreach (KeyValuePair<string, string> define in PreprocessorDefines)
        {
            request.AddPreprocessorDefine(define.Key, define.Value);
        }

        if (request.ProcessCommandLineArguments(args).IsError)
        {
            throw new Exception("Failed to process command line arguments");
        }

        if (request.Compile().IsError)
        {
            throw new Exception("Compilation failed");
        }

        reflectionJson = request.GetReflectionJson();

        return request.GetResult();
    }

    private static void DiagnosticCallback(char* message, void* userData)
    {
        Error?.Invoke(Marshal.PtrToStringAnsi((nint)message) ?? string.Empty);
    }
}
