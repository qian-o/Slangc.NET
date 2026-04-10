using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Slangc.NET;

/// <summary>
/// Provides static methods for compiling Slang shader code using the Slang compiler.
/// This is the main entry point for the Slangc.NET library.
/// </summary>
public static unsafe class SlangCompiler
{
    /// <summary>
    /// Shared Slang session instance used for all compilation requests.
    /// </summary>
    private static readonly SlangSession session;

    static SlangCompiler()
    {
        nint slangCompiler;

        string architecture = RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant();

        if (OperatingSystem.IsWindows())
        {
            slangCompiler = Load(
                Path.Combine(AppContext.BaseDirectory, "runtimes", $"win-{architecture}", "native", "slang-compiler.dll"),
                Path.Combine(AppContext.BaseDirectory, "slang-compiler.dll"),
                "slang-compiler.dll"
            );
        }
        else if (OperatingSystem.IsLinux())
        {
            slangCompiler = Load(
                Path.Combine(AppContext.BaseDirectory, "runtimes", $"linux-{architecture}", "native", "libslang-compiler.so"),
                Path.Combine(AppContext.BaseDirectory, "libslang-compiler.so"),
                "libslang-compiler.so"
            );
        }
        else if (OperatingSystem.IsMacOS())
        {
            slangCompiler = Load(
                Path.Combine(AppContext.BaseDirectory, "runtimes", $"osx-{architecture}", "native", "libslang-compiler.dylib"),
                Path.Combine(AppContext.BaseDirectory, "libslang-compiler.dylib"),
                "libslang-compiler.dylib"
            );
        }
        else
        {
            throw new PlatformNotSupportedException("Slangc.NET is not supported on this platform.");
        }

        NativeLibrary.SetDllImportResolver(typeof(SlangCompiler).Assembly, (_, _, _) => slangCompiler);

        session = new();

        static nint Load(params string[] paths)
        {
            foreach (string path in paths)
            {
                if (File.Exists(path) && NativeLibrary.TryLoad(path, out nint handle))
                {
                    return handle;
                }
            }

            return 0;
        }
    }

    /// <summary>
    /// Compiles Slang shader code with the specified command line arguments.
    /// </summary>
    /// <param name="args">Command line arguments for the Slang compiler (e.g., file paths, target profiles, etc.)</param>
    /// <returns>The compiled shader bytecode as a byte array</returns>
    /// <exception cref="Exception">Thrown when compilation fails with diagnostic messages</exception>
    public static byte[] Compile(params string[] args)
    {
        using SlangCompileRequest request = session.CreateCompileRequest();

        Compile(request, args);

        return request.GetResult();
    }

    /// <summary>
    /// Compiles Slang shader code with the specified command line arguments and returns reflection information.
    /// </summary>
    /// <param name="args">Command line arguments for the Slang compiler (e.g., file paths, target profiles, etc.)</param>
    /// <param name="reflection">Outputs reflection information about the compiled shader including parameters and entry points</param>
    /// <returns>The compiled shader bytecode as a byte array</returns>
    /// <exception cref="Exception">Thrown when compilation fails with diagnostic messages</exception>
    public static byte[] CompileWithReflection(string[] args, out SlangReflection reflection)
    {
        using SlangCompileRequest request = session.CreateCompileRequest();

        Compile(request, args);

        reflection = new(request.Handle);

        return request.GetResult();
    }

    /// <summary>
    /// Internal method that performs the actual compilation with the given compile request and arguments.
    /// Sets up diagnostic callbacks and handles compilation errors.
    /// </summary>
    /// <param name="request">The compile request to use for compilation</param>
    /// <param name="args">Command line arguments to pass to the compiler</param>
    /// <returns>The same compile request after processing</returns>
    /// <exception cref="Exception">Thrown when command line processing or compilation fails</exception>
    private static SlangCompileRequest Compile(SlangCompileRequest request, string[] args)
    {
        StringBuilder sb = new();

        request.SetDiagnosticCallback(&DiagnosticCallback, &sb);

        if (request.ProcessCommandLineArguments(args) is not 0)
        {
            throw new Exception(sb.ToString());
        }

        if (request.Compile() is not 0)
        {
            throw new Exception(sb.ToString());
        }

        return request;
    }

    /// <summary>
    /// Callback function for receiving diagnostic messages from the Slang compiler.
    /// Appends diagnostic messages to the StringBuilder stored in userData.
    /// </summary>
    /// <param name="message">Pointer to the diagnostic message string from Slang</param>
    /// <param name="userData">Pointer to user data (StringBuilder*)</param>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void DiagnosticCallback(byte* message, void* userData)
    {
        (*(StringBuilder*)userData).Append(Marshal.PtrToStringUTF8((nint)message) ?? "");
    }
}
