using System.Runtime.InteropServices;
using System.Text;

namespace Slangc.NET;

public static unsafe class SlangCompiler
{
    private static readonly SlangSession session = new();

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
        StringBuilder stringBuilder = new();
        GCHandle handle = GCHandle.Alloc(stringBuilder);

        try
        {
            request.SetDiagnosticCallback(DiagnosticCallback, null);

            if (request.ProcessCommandLineArguments(args).IsError)
            {
                throw new Exception(stringBuilder.ToString());
            }

            if (request.Compile().IsError)
            {
                throw new Exception(stringBuilder.ToString());
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
        GCHandle handle = GCHandle.FromIntPtr((nint)userData);

        ((StringBuilder)handle.Target!).Append(Marshal.PtrToStringAnsi((nint)message) ?? string.Empty);
    }
}
