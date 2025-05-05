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

    public SlangReflection(nint request, bool parseJson)
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

        Json = Marshal.PtrToStringAnsi((nint)outBlob->GetBufferPointer(), (int)outBlob->GetBufferSize()) ?? string.Empty;

        if (parseJson && !string.IsNullOrEmpty(Json))
        {
            using JsonDocument document = JsonDocument.Parse(Json);

            JsonObject reader = JsonObject.Create(document.RootElement)!;

            Parameters = [.. reader["parameters"]!.AsArray().Select(static reader => new SlangParameter(reader!.AsObject()))];
            EntryPoints = [.. reader["entryPoints"]!.AsArray().Select(static reader => new SlangEntryPoint(reader!.AsObject()))];
        }
    }

    public string Json { get; } = string.Empty;

    public SlangParameter[] Parameters { get; } = [];

    public SlangEntryPoint[] EntryPoints { get; } = [];
}
