using System.Runtime.InteropServices;

namespace Slangc.NET;

/// <summary>
/// Represents a Slang compilation session that manages the lifetime of the underlying Slang session.
/// A session can be used to create multiple compile requests.
/// </summary>
public unsafe partial class SlangSession : IDisposable
{
    /// <summary>
    /// Native function to create a new Slang session.
    /// </summary>
    /// <param name="lpString">Optional configuration string for the session</param>
    /// <returns>Handle to the created Slang session</returns>
    [LibraryImport("slang")]
    private static partial nint spCreateSession(char* lpString);

    /// <summary>
    /// Native function to destroy a Slang session.
    /// </summary>
    /// <param name="session">Handle to the Slang session to destroy</param>
    [LibraryImport("slang")]
    private static partial void spDestroySession(nint session);

    /// <summary>
    /// Native function to create a compile request within a session.
    /// </summary>
    /// <param name="session">Handle to the Slang session</param>
    /// <returns>Handle to the created compile request</returns>
    [LibraryImport("slang")]
    private static partial nint spCreateCompileRequest(nint session);

    /// <summary>
    /// Gets the native handle to the underlying Slang session.
    /// </summary>
    public nint Handle { get; } = spCreateSession(null);

    /// <summary>
    /// Creates a new compile request associated with this session.
    /// </summary>
    /// <returns>A new SlangCompileRequest instance</returns>
    public SlangCompileRequest CreateCompileRequest()
    {
        return new(spCreateCompileRequest(Handle));
    }

    /// <summary>
    /// Disposes the session and releases associated native resources.
    /// </summary>
    public void Dispose()
    {
        if (Handle is not 0)
        {
            spDestroySession(Handle);
        }

        GC.SuppressFinalize(this);
    }
}
