using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Slangc.NET;

/// <summary>
/// Provides reflection information about compiled shaders, including parameters, entry points, and JSON metadata.
/// This class allows introspection of shader structure and binding information.
/// </summary>
public unsafe partial class SlangReflection
{
    /// <summary>
    /// Native function to get reflection information from a compile request.
    /// </summary>
    /// <param name="request">Handle to the compile request</param>
    /// <returns>Handle to the reflection data</returns>
    [LibraryImport("slang")]
    private static partial nint spGetReflection(nint request);

    /// <summary>
    /// Native function to convert reflection data to JSON format.
    /// </summary>
    /// <param name="reflection">Handle to the reflection data</param>
    /// <param name="request">Handle to the compile request</param>
    /// <param name="outBlob">Pointer to receive the output blob containing JSON data</param>
    /// <returns>Result code (0 for success)</returns>
    [LibraryImport("slang")]
    private static partial int spReflection_ToJson(nint reflection, nint request, SlangBlob** outBlob);

    /// <summary>
    /// Initializes a new instance of the SlangReflection class from a compile request.
    /// </summary>
    /// <param name="request">Handle to the compile request to extract reflection from</param>
    /// <param name="parseJson">Whether to parse the JSON reflection data into structured objects</param>
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

    /// <summary>
    /// Gets the reflection information as a JSON string.
    /// </summary>
    public string Json { get; } = string.Empty;

    /// <summary>
    /// Gets the array of shader parameters parsed from the reflection data.
    /// This includes uniform buffers, textures, samplers, and other binding resources.
    /// </summary>
    public SlangParameter[] Parameters { get; } = [];

    /// <summary>
    /// Gets the array of entry points parsed from the reflection data.
    /// Each entry point represents a shader stage (vertex, fragment, compute, etc.).
    /// </summary>
    public SlangEntryPoint[] EntryPoints { get; } = [];
}
