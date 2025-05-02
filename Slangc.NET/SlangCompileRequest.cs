using System.Runtime.InteropServices;

namespace Slangc.NET;

public unsafe partial class SlangCompileRequest(nint handle) : IDisposable
{
    public delegate void SlangDiagnosticCallback(char* message, void* userData);

    [LibraryImport("slang")]
    private static partial void spDestroyCompileRequest(nint request);

    [LibraryImport("slang")]
    private static partial void spSetDiagnosticCallback(nint request, void* callback, void* userData);

    [LibraryImport("slang")]
    private static partial void spAddSearchPath(nint request, char* path);

    [LibraryImport("slang")]
    private static partial void spAddPreprocessorDefine(nint request, char* key, char* value);

    [LibraryImport("slang")]
    private static partial int spProcessCommandLineArguments(nint request, char** args, int argCount);

    [LibraryImport("slang")]
    private static partial int spCompile(nint request);

    [LibraryImport("slang")]
    private static partial char* spGetCompileRequestCode(nint request, uint* outSize);

    public nint Handle { get; } = handle;

    public void SetDiagnosticCallback(SlangDiagnosticCallback callback, void* userData)
    {
        spSetDiagnosticCallback(Handle, (void*)Marshal.GetFunctionPointerForDelegate(callback), userData);
    }

    public void AddSearchPath(string path)
    {
        char* pathPtr = (char*)Marshal.StringToHGlobalAnsi(path);

        spAddSearchPath(Handle, pathPtr);

        Marshal.FreeHGlobal((nint)pathPtr);
    }

    public void AddPreprocessorDefine(string key, string value)
    {
        char* keyPtr = (char*)Marshal.StringToHGlobalAnsi(key);
        char* valuePtr = (char*)Marshal.StringToHGlobalAnsi(value);

        spAddPreprocessorDefine(Handle, keyPtr, valuePtr);

        Marshal.FreeHGlobal((nint)keyPtr);
        Marshal.FreeHGlobal((nint)valuePtr);
    }

    public int ProcessCommandLineArguments(string[] args)
    {
        char** argsPtr = stackalloc char*[args.Length];

        for (int i = 0; i < args.Length; i++)
        {
            argsPtr[i] = (char*)Marshal.StringToHGlobalAnsi(args[i]);
        }

        int result = spProcessCommandLineArguments(Handle, argsPtr, args.Length);

        for (int i = 0; i < args.Length; i++)
        {
            Marshal.FreeHGlobal((nint)argsPtr[i]);
        }

        return result;
    }

    public int Compile()
    {
        return spCompile(Handle);
    }

    public byte[] GetResult()
    {
        uint size;
        char* codePtr = spGetCompileRequestCode(Handle, &size);

        return [.. new ReadOnlySpan<byte>(codePtr, (int)size)];
    }

    public void Dispose()
    {
        if (Handle is not 0)
        {
            spDestroyCompileRequest(Handle);
        }

        GC.SuppressFinalize(this);
    }
}
