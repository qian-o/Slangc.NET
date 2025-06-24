using System.Runtime.InteropServices;

namespace Slangc.NET;

/// <summary>
/// Represents a Slang compilation request that can be configured and executed to compile shader code.
/// This class wraps the native Slang compile request and provides a .NET API for shader compilation.
/// </summary>
public unsafe partial class SlangCompileRequest(nint handle) : IDisposable
{
    /// <summary>
    /// Delegate for receiving diagnostic messages during compilation.
    /// </summary>
    /// <param name="message">Pointer to the diagnostic message string</param>
    /// <param name="userData">Pointer to user-provided data</param>
    public delegate void SlangDiagnosticCallback(char* message, void* userData);

    /// <summary>
    /// Native function to destroy a compile request.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    [LibraryImport("slang")]
    private static partial void spDestroyCompileRequest(nint request);

    /// <summary>
    /// Native function to set a diagnostic callback for the compile request.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    /// <param name="callback">Pointer to the callback function</param>
    /// <param name="userData">Pointer to user data passed to the callback</param>
    [LibraryImport("slang")]
    private static partial void spSetDiagnosticCallback(nint request, void* callback, void* userData);

    /// <summary>
    /// Native function to add a search path for includes and imports.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    /// <param name="path">Pointer to the path string</param>
    [LibraryImport("slang")]
    private static partial void spAddSearchPath(nint request, char* path);

    /// <summary>
    /// Native function to add a preprocessor define.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    /// <param name="key">Pointer to the define key string</param>
    /// <param name="value">Pointer to the define value string</param>
    [LibraryImport("slang")]
    private static partial void spAddPreprocessorDefine(nint request, char* key, char* value);

    /// <summary>
    /// Native function to process command line arguments.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    /// <param name="args">Pointer to array of argument strings</param>
    /// <param name="argCount">Number of arguments</param>
    /// <returns>Result code (0 for success)</returns>
    [LibraryImport("slang")]
    private static partial int spProcessCommandLineArguments(nint request, char** args, int argCount);

    /// <summary>
    /// Native function to execute the compilation.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    /// <returns>Result code (0 for success)</returns>
    [LibraryImport("slang")]
    private static partial int spCompile(nint request);

    /// <summary>
    /// Native function to get the compiled result.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    /// <param name="outSize">Pointer to receive the output size</param>
    /// <returns>Pointer to the compiled bytecode</returns>
    [LibraryImport("slang")]
    private static partial char* spGetCompileRequestCode(nint request, uint* outSize);

    /// <summary>
    /// Gets the native handle to the underlying Slang compile request.
    /// </summary>
    public nint Handle { get; } = handle;

    /// <summary>
    /// Sets a callback function to receive diagnostic messages during compilation.
    /// </summary>
    /// <param name="callback">The callback function to receive diagnostic messages</param>
    /// <param name="userData">User data pointer that will be passed to the callback</param>
    public void SetDiagnosticCallback(SlangDiagnosticCallback callback, void* userData)
    {
        spSetDiagnosticCallback(Handle, (void*)Marshal.GetFunctionPointerForDelegate(callback), userData);
    }

    /// <summary>
    /// Adds a search path for resolving include directives and import statements.
    /// </summary>
    /// <param name="path">The directory path to add to the search path list</param>
    public void AddSearchPath(string path)
    {
        char* pathPtr = (char*)Marshal.StringToHGlobalAnsi(path);

        spAddSearchPath(Handle, pathPtr);

        Marshal.FreeHGlobal((nint)pathPtr);
    }

    /// <summary>
    /// Adds a preprocessor define that will be available during shader compilation.
    /// </summary>
    /// <param name="key">The name of the preprocessor define</param>
    /// <param name="value">The value to assign to the preprocessor define</param>
    public void AddPreprocessorDefine(string key, string value)
    {
        char* keyPtr = (char*)Marshal.StringToHGlobalAnsi(key);
        char* valuePtr = (char*)Marshal.StringToHGlobalAnsi(value);

        spAddPreprocessorDefine(Handle, keyPtr, valuePtr);

        Marshal.FreeHGlobal((nint)keyPtr);
        Marshal.FreeHGlobal((nint)valuePtr);
    }

    /// <summary>
    /// Processes command line arguments to configure the compilation request.
    /// </summary>
    /// <param name="args">Array of command line arguments (e.g., file paths, compiler options)</param>
    /// <returns>Result code where 0 indicates success</returns>
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

    /// <summary>
    /// Executes the shader compilation based on the configured parameters.
    /// </summary>
    /// <returns>Result code where 0 indicates successful compilation</returns>
    public int Compile()
    {
        return spCompile(Handle);
    }

    /// <summary>
    /// Retrieves the compiled shader bytecode as a byte array.
    /// </summary>
    /// <returns>The compiled shader bytecode</returns>
    public byte[] GetResult()
    {
        uint size;
        char* codePtr = spGetCompileRequestCode(Handle, &size);

        return [.. new ReadOnlySpan<byte>(codePtr, (int)size)];
    }

    /// <summary>
    /// Disposes the compile request and releases associated native resources.
    /// </summary>
    public void Dispose()
    {
        if (Handle is not 0)
        {
            spDestroyCompileRequest(Handle);
        }

        GC.SuppressFinalize(this);
    }
}
