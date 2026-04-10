using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Slangc.NET;

/// <summary>
/// Represents a Slang compilation request that can be configured and executed to compile shader code.
/// This class wraps the native Slang compile request and provides a .NET API for shader compilation.
/// </summary>
public unsafe partial class SlangCompileRequest(nint handle) : IDisposable
{
    /// <summary>
    /// Native function to destroy a compile request.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    [LibraryImport("slang-compiler")]
    private static partial void spDestroyCompileRequest(nint request);

    /// <summary>
    /// Native function to set a diagnostic callback for the compile request.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    /// <param name="callback">Pointer to the callback function</param>
    /// <param name="userData">Pointer to user data passed to the callback</param>
    [LibraryImport("slang-compiler")]
    private static partial void spSetDiagnosticCallback(nint request, void* callback, void* userData);

    /// <summary>
    /// Native function to add a search path for includes and imports.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    /// <param name="path">Pointer to the path string</param>
    [LibraryImport("slang-compiler")]
    private static partial void spAddSearchPath(nint request, byte* path);

    /// <summary>
    /// Native function to add a preprocessor define.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    /// <param name="key">Pointer to the define key string</param>
    /// <param name="value">Pointer to the define value string</param>
    [LibraryImport("slang-compiler")]
    private static partial void spAddPreprocessorDefine(nint request, byte* key, byte* value);

    /// <summary>
    /// Native function to process command line arguments.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    /// <param name="args">Pointer to array of argument strings</param>
    /// <param name="argCount">Number of arguments</param>
    /// <returns>Result code (0 for success)</returns>
    [LibraryImport("slang-compiler")]
    private static partial int spProcessCommandLineArguments(nint request, byte** args, int argCount);

    /// <summary>
    /// Native function to execute the compilation.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    /// <returns>Result code (0 for success)</returns>
    [LibraryImport("slang-compiler")]
    private static partial int spCompile(nint request);

    /// <summary>
    /// Native function to get the compiled code for a specific entry point.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    /// <param name="entryPointIndex">Index of the entry point</param>
    /// <param name="outSize">Pointer to receive the output size</param>
    /// <returns>Pointer to the compiled code</returns>
    [LibraryImport("slang-compiler")]
    private static partial byte* spGetEntryPointCode(nint request, int entryPointIndex, uint* outSize);

    /// <summary>
    /// Native function to get the compiled result.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    /// <param name="outSize">Pointer to receive the output size</param>
    /// <returns>Pointer to the compiled bytecode</returns>
    [LibraryImport("slang-compiler")]
    private static partial byte* spGetCompileRequestCode(nint request, uint* outSize);

    /// <summary>
    /// Gets the native handle to the underlying Slang compile request.
    /// </summary>
    public nint Handle { get; } = handle;

    /// <summary>
    /// Sets a callback function to receive diagnostic messages during compilation.
    /// </summary>
    /// <param name="callback">The callback function to receive diagnostic messages</param>
    /// <param name="userData">User data pointer that will be passed to the callback</param>
    public void SetDiagnosticCallback(delegate* unmanaged[Cdecl]<byte*, void*, void> callback, void* userData)
    {
        spSetDiagnosticCallback(Handle, callback, userData);
    }

    /// <summary>
    /// Adds a search path for resolving include directives and import statements.
    /// </summary>
    /// <param name="path">The directory path to add to the search path list</param>
    public void AddSearchPath(string path)
    {
        using var pathUtf8 = TmpUtf8String.Alloc(path);
        fixed (byte* pathPtr = pathUtf8)
        {
            spAddSearchPath(Handle, pathPtr);
        }
    }

    /// <summary>
    /// Adds a preprocessor define that will be available during shader compilation.
    /// </summary>
    /// <param name="key">The name of the preprocessor define</param>
    /// <param name="value">The value to assign to the preprocessor define</param>
    public void AddPreprocessorDefine(string key, string value)
    {
        using var keyUtf8 = TmpUtf8String.Alloc(key);
        using var valueUtf8 = TmpUtf8String.Alloc(value);
        fixed (byte* keyPtr = keyUtf8)
        fixed (byte* valuePtr = valueUtf8)
        {
            spAddPreprocessorDefine(Handle, keyPtr, valuePtr);
        }
    }

    /// <inheritdoc cref="ProcessCommandLineArguments(ReadOnlySpan{string})"/>
    public int ProcessCommandLineArguments(string[] args) => ProcessCommandLineArguments(args.AsSpan());

    /// <summary>
    /// Processes command line arguments to configure the compilation request.
    /// </summary>
    /// <param name="args">Array of command line arguments (e.g., file paths, compiler options)</param>
    /// <returns>Result code where 0 indicates success</returns>
    public int ProcessCommandLineArguments(params ReadOnlySpan<string> args)
    {
        var sum_utf8_length = 0;
        foreach (var arg in args) sum_utf8_length += Encoding.UTF8.GetMaxByteCount(arg.Length) + 1;
        using var tmp = TmpUtf8String.Rent(sum_utf8_length);
        fixed (byte* ptr = tmp)
        {
            var offset = 0;
            var argsPtr = stackalloc byte*[args.Length];
            for (var i = 0; i < args.Length; i++)
            {
                var span = tmp.Data.AsSpan(offset);
                var count = Encoding.UTF8.GetBytes(args[i], span);
                (argsPtr[i] = ptr + offset)[count] = 0;
                offset += count + 1;
            }
            return spProcessCommandLineArguments(Handle, argsPtr, args.Length);
        }
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
        spGetEntryPointCode(Handle, 0, &size);

        if (size is not 0)
        {
            List<byte> codes = [];

            int i = 0;
            while (true)
            {
                byte* codePtr = spGetEntryPointCode(Handle, i++, &size);

                if (size is 0)
                {
                    break;
                }

                codes.AddRange(new ReadOnlySpan<byte>(codePtr, (int)size));
            }

            return [.. codes];
        }
        else
        {
            byte* codePtr = spGetCompileRequestCode(Handle, &size);

            return [.. new ReadOnlySpan<byte>(codePtr, (int)size)];
        }
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
