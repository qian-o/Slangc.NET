using System.Runtime.InteropServices;

namespace Slangc.NET;

public unsafe partial class SlangReflection
{
    [LibraryImport("slang")]
    private static partial nint spGetReflection(nint request);

    [LibraryImport("slang")]
    private static partial SlangResult spReflection_ToJson(nint reflection, nint request, SlangBlob** outBlob);

    public SlangReflection(nint request)
    {
        nint reflection = spGetReflection(request);

        if (reflection is 0)
        {
            return;
        }

        SlangBlob* outBlob;
        if (spReflection_ToJson(reflection, request, &outBlob).IsError)
        {
            return;
        }

        void* buffer = outBlob->GetBufferPointer();
        ulong size = outBlob->GetBufferSize();

        Json = Marshal.PtrToStringAnsi((nint)buffer, (int)size) ?? string.Empty;
    }

    public string Json { get; } = string.Empty;
}
