using System.Runtime.InteropServices;
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
    [LibraryImport("slang-compiler")]
    private static partial nint spGetReflection(nint request);

    /// <summary>
    /// Native function to convert reflection data to JSON format.
    /// </summary>
    /// <param name="reflection">Handle to the reflection data</param>
    /// <param name="request">Handle to the compile request</param>
    /// <param name="outBlob">Pointer to receive the output blob containing JSON data</param>
    /// <returns>Result code (0 for success)</returns>
    [LibraryImport("slang-compiler")]
    private static partial int spReflection_ToJson(nint reflection, nint request, SlangBlob** outBlob);

    /// <summary>
    /// Native function to get the number of entry points in reflection data.
    /// </summary>
    /// <param name="reflection">Handle to the reflection data</param>
    /// <returns>Number of reflected entry points</returns>
    [LibraryImport("slang-compiler")]
    private static partial nuint spReflection_getEntryPointCount(nint reflection);

    /// <summary>
    /// Native function to get an entry point from reflection data by index.
    /// </summary>
    /// <param name="reflection">Handle to the reflection data</param>
    /// <param name="index">Zero-based index of the entry point</param>
    /// <returns>Handle to the reflected entry point</returns>
    [LibraryImport("slang-compiler")]
    private static partial nint spReflection_getEntryPointByIndex(nint reflection, nuint index);

    /// <summary>
    /// Native function to get the thread group dimensions of an entry point.
    /// </summary>
    /// <param name="entryPoint">Handle to the reflected entry point</param>
    /// <param name="axisCount">Number of dimensions to write</param>
    /// <param name="outSizeAlongAxis">Pointer to the output dimensions</param>
    [LibraryImport("slang-compiler")]
    private static partial void spReflectionEntryPoint_getComputeThreadGroupSize(nint entryPoint, nuint axisCount, nuint* outSizeAlongAxis);

    private readonly Lazy<(string Version, SlangScope? GlobalScope, SlangParameter[] Parameters, SlangEntryPoint[] EntryPoints)>? deserialized;

    /// <summary>
    /// Initializes a new instance of the SlangReflection class from a compile request.
    /// </summary>
    /// <param name="request">Handle to the compile request to extract reflection from</param>
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

        uint[][] threadGroupSizes = GetThreadGroupSizes(reflection);

        Json = Marshal.PtrToStringUTF8((nint)outBlob->GetBufferPointer(), (int)outBlob->GetBufferSize()) ?? "";

        deserialized = new(() =>
        {
            try
            {
                JsonObject reader = JsonNode.Parse(Json)!.AsObject();

                string version = reader.ContainsKey("version") ? reader["version"].Deserialize<string>() : "1.0";
                SlangScope? globalScope = reader.ContainsKey("globalScope") ? new(reader["globalScope"]!.AsObject()) : null;
                SlangParameter[] parameters = reader.ContainsKey("parameters") ? [.. reader["parameters"]!.AsArray().Select(static reader => new SlangParameter(reader!.AsObject()))] : [];
                SlangEntryPoint[] entryPoints = reader.ContainsKey("entryPoints") ? [.. reader["entryPoints"]!.AsArray().Select(static reader => new SlangEntryPoint(reader!.AsObject()))] : [];

                for (int i = 0; i < entryPoints.Length && i < threadGroupSizes.Length; i++)
                {
                    if ((entryPoints[i].Stage is SlangStage.Compute or SlangStage.Mesh or SlangStage.Amplification or SlangStage.Node) && entryPoints[i].ThreadGroupSize.Length is 0 && threadGroupSizes[i].Length is 3)
                    {
                        entryPoints[i].ThreadGroupSize = threadGroupSizes[i];
                    }
                }

                return (version, globalScope, parameters, entryPoints);
            }
            catch
            {
                return ("1.0", null, [], []);
            }
        });
    }

    /// <summary>
    /// Gets the reflection information as a JSON string.
    /// </summary>
    public string Json { get; } = string.Empty;

    /// <summary>
    /// Gets the Slang reflection JSON schema version. JSON produced before schema version 1.1 is reported as 1.0.
    /// </summary>
    public string Version => deserialized?.Value.Version ?? "1.0";

    /// <summary>
    /// Gets the complete global parameter scope, when the JSON contains one.
    /// </summary>
    public SlangScope? GlobalScope => deserialized?.Value.GlobalScope;

    /// <summary>
    /// Gets the array of shader parameters parsed from the reflection data.
    /// This includes uniform buffers, textures, samplers, and other binding resources.
    /// </summary>
    public SlangParameter[] Parameters => deserialized?.Value.Parameters ?? [];

    /// <summary>
    /// Gets the array of entry points parsed from the reflection data.
    /// Each entry point represents a shader stage (vertex, fragment, compute, etc.).
    /// </summary>
    public SlangEntryPoint[] EntryPoints => deserialized?.Value.EntryPoints ?? [];

    /// <summary>
    /// Copies thread group dimensions from native reflection data before the compile request is released.
    /// </summary>
    /// <param name="reflection">Handle to the reflection data</param>
    /// <returns>
    /// Thread group dimensions indexed in the same order as the reflected entry points.
    /// Entries without valid dimensions contain an empty array.
    /// </returns>
    private static uint[][] GetThreadGroupSizes(nint reflection)
    {
        uint[][] threadGroupSizes = new uint[spReflection_getEntryPointCount(reflection)][];

        nuint* size = stackalloc nuint[3];
        for (int i = 0; i < threadGroupSizes.Length; i++)
        {
            size[0] = size[1] = size[2] = 0;
            spReflectionEntryPoint_getComputeThreadGroupSize(spReflection_getEntryPointByIndex(reflection, (nuint)i), 3, size);

            if (size[0] is not 0 && size[1] is not 0 && size[2] is not 0)
            {
                threadGroupSizes[i] = [(uint)size[0], (uint)size[1], (uint)size[2]];
            }
            else
            {
                threadGroupSizes[i] = [];
            }
        }

        return threadGroupSizes;
    }
}
