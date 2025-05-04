using System.Runtime.InteropServices;

namespace Slangc.NET;

public unsafe partial class SlangSession : IDisposable
{
    [LibraryImport("slang")]
    private static partial nint spCreateSession(char* lpString);

    [LibraryImport("slang")]
    private static partial void spDestroySession(nint session);

    [LibraryImport("slang")]
    private static partial nint spCreateCompileRequest(nint session);

    public nint Handle { get; } = spCreateSession(null);

    public SlangCompileRequest CreateCompileRequest()
    {
        return new(spCreateCompileRequest(Handle));
    }

    public void Dispose()
    {
        if (Handle is not 0)
        {
            spDestroySession(Handle);
        }

        GC.SuppressFinalize(this);
    }
}
