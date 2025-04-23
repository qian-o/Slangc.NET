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
    private static partial SlangResult spProcessCommandLineArguments(nint request, char** args, int argCount);

    [LibraryImport("slang")]
    private static partial SlangResult spCompile(nint request);

    [LibraryImport("slang")]
    private static partial char* spGetCompileRequestCode(nint request, uint* outSize);

    [LibraryImport("slang")]
    private static partial nint spGetReflection(nint request);

    [LibraryImport("slang")]
    private static partial SlangResult spReflection_ToJson(nint reflection, nint request, ISlangBlob** outBlob);

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

    public SlangResult ProcessCommandLineArguments(string[] args)
    {
        char** argsPtr = stackalloc char*[args.Length];

        for (int i = 0; i < args.Length; i++)
        {
            argsPtr[i] = (char*)Marshal.StringToHGlobalAnsi(args[i]);
        }

        SlangResult result = spProcessCommandLineArguments(Handle, argsPtr, args.Length);

        for (int i = 0; i < args.Length; i++)
        {
            Marshal.FreeHGlobal((nint)argsPtr[i]);
        }

        return result;
    }

    public SlangResult Compile()
    {
        return spCompile(Handle);
    }

    public byte[] GetResult()
    {
        uint size;
        char* codePtr = spGetCompileRequestCode(Handle, &size);

        return [.. new ReadOnlySpan<byte>(codePtr, (int)size)];
    }

    public string GetReflectionJson()
    {
        ISlangBlob* outBlob;

        spReflection_ToJson(spGetReflection(Handle), Handle, &outBlob);

        void* buffer = outBlob->GetBufferPointer();
        ulong size = outBlob->GetBufferSize();

        return Marshal.PtrToStringAnsi((nint)buffer, (int)size);
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
