using System.Text.Json.Nodes;

namespace Slangc.NET;

/// <summary>
/// Represents a shader parameter that can be bound to a pipeline, such as uniform buffers, textures, or samplers.
/// </summary>
public class SlangParameter
{
    /// <summary>
    /// Initializes a new instance of the SlangParameter class from JSON reflection data.
    /// </summary>
    /// <param name="reader">JSON object containing parameter information</param>
    internal SlangParameter(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        UserAttributes = reader.ContainsKey("userAttribs") ? [.. reader["userAttribs"]!.AsArray().Select(static reader => new SlangUserAttribute(reader!.AsObject()))] : [];
        Bindings = reader.ContainsKey("bindings") ? [.. reader["bindings"]!.AsArray().Select(static reader => new SlangBinding(reader!.AsObject()))] : reader.ContainsKey("binding") ? [new(reader["binding"]!.AsObject())] : [];
        Type = new(reader["type"]!.AsObject());
        Shared = reader["shared"].Deserialize<bool>();
        Stage = reader["stage"].Deserialize<SlangStage>();
        SemanticName = reader["semanticName"].Deserialize<string>();
        SemanticIndex = reader["semanticIndex"].Deserialize<uint>();
        Format = reader["format"].Deserialize<string>();
    }

    /// <summary>
    /// Gets the name of the parameter.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the user-defined attributes associated with this parameter.
    /// </summary>
    public SlangUserAttribute[] UserAttributes { get; }

    /// <summary>
    /// Gets all binding points associated with this parameter.
    /// </summary>
    public SlangBinding[] Bindings { get; }

    /// <summary>
    /// Gets the reflected parameter type.
    /// </summary>
    public SlangType Type { get; }

    /// <summary>
    /// Gets a value indicating whether the parameter is shared.
    /// </summary>
    public bool Shared { get; }

    /// <summary>
    /// Gets the shader stage associated with this parameter, when applicable.
    /// </summary>
    public SlangStage Stage { get; }

    /// <summary>
    /// Gets the semantic name associated with this parameter, when applicable.
    /// </summary>
    public string? SemanticName { get; }

    /// <summary>
    /// Gets the semantic index associated with this parameter.
    /// </summary>
    public uint SemanticIndex { get; }

    /// <summary>
    /// Gets the image format associated with this parameter, when available.
    /// </summary>
    public string? Format { get; }
}
