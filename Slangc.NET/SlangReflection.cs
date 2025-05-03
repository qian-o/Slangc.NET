using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using Slangc.NET.Models;

namespace Slangc.NET;

public unsafe partial class SlangReflection
{
    [LibraryImport("slang")]
    private static partial nint spGetReflection(nint request);

    [LibraryImport("slang")]
    private static partial int spReflection_ToJson(nint reflection, nint request, SlangBlob** outBlob);

    public SlangReflection(nint request)
    {
        nint reflection = spGetReflection(request);

        if (reflection is 0)
        {
            return;
        }

        SlangBlob* outBlob;
        if (spReflection_ToJson(reflection, request, &outBlob) is not 0)
        {
            return;
        }

        void* buffer = outBlob->GetBufferPointer();
        ulong size = outBlob->GetBufferSize();

        Json = Marshal.PtrToStringAnsi((nint)buffer, (int)size) ?? string.Empty;

        if (string.IsNullOrEmpty(Json))
        {
            return;
        }

        using JsonDocument document = JsonDocument.Parse(Json);

        JsonObject reader = JsonObject.Create(document.RootElement)!;

        Parameters = [.. reader["parameters"]!.AsArray().Select(static reader => new Parameter(reader!.AsObject()))];
    }

    public string Json { get; } = string.Empty;

    public Parameter[] Parameters { get; } = [];
}
