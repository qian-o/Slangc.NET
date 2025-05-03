using System.Runtime.InteropServices;
using System.Text;

namespace Slangc.NET;

public static unsafe class SlangCompiler
{
    private static readonly SlangSession session = new();

    public static bool EnableDeserialization { get; set; }

    public static byte[] Compile(params string[] args)
    {
        using SlangCompileRequest request = session.CreateCompileRequest();

        Compile(request, args);

        return request.GetResult();
    }

    public static byte[] CompileWithReflection(string[] args, out SlangReflection reflection)
    {
        using SlangCompileRequest request = session.CreateCompileRequest();

        Compile(request, args);

        reflection = new(request.Handle);

        return request.GetResult();
    }

    private static SlangCompileRequest Compile(SlangCompileRequest request, string[] args)
    {
        StringBuilder sb = new();

        GCHandle handle = GCHandle.Alloc(sb);

        try
        {
            request.SetDiagnosticCallback(DiagnosticCallback, (void*)(nint)handle);

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
        finally
        {
            handle.Free();
        }
    }

    private static void DiagnosticCallback(char* message, void* userData)
    {
        GCHandle handle = (GCHandle)(nint)userData;

        ((StringBuilder)handle.Target!).Append(Marshal.PtrToStringAnsi((nint)message) ?? string.Empty);
    }
}
