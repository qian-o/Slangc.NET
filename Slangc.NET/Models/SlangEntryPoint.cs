using System.Text.Json.Nodes;

namespace Slangc.NET;

/// <summary>
/// Represents a shader entry point (such as vertex, fragment, or compute shader stages) with its associated bindings and metadata.
/// </summary>
public class SlangEntryPoint
{
    /// <summary>
    /// Initializes a new instance of the SlangEntryPoint class from JSON reflection data.
    /// </summary>
    /// <param name="reader">JSON object containing entry point information</param>
    internal SlangEntryPoint(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Stage = reader["stage"].Deserialize<SlangStage>();
        ThreadGroupSize = reader.ContainsKey("threadGroupSize") ? [.. reader["threadGroupSize"]!.AsArray().Select(static reader => reader!.Deserialize<uint>())] : [];
        Bindings = [.. reader["bindings"]!.AsArray().Select(static reader => new SlangNamedTypeBinding(reader!.AsObject()))];
    }

    /// <summary>
    /// Gets the name of the entry point function.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the shader stage this entry point represents (vertex, fragment, compute, etc.).
    /// </summary>
    public SlangStage Stage { get; }

    /// <summary>
    /// Gets the thread group size for compute shaders (if applicable).
    /// This array contains the X, Y, Z dimensions of the thread group.
    /// </summary>
    public uint[] ThreadGroupSize { get; set; }

    /// <summary>
    /// Gets the named type bindings associated with this entry point.
    /// These represent the resources that need to be bound when using this entry point.
    /// </summary>
    public SlangNamedTypeBinding[] Bindings { get; set; }
}
