using System.Text.Json.Nodes;

namespace Slangc.NET;

/// <summary>
/// Represents a variable in a shader with its type and optional binding information.
/// </summary>
public class SlangVar
{
    /// <summary>
    /// Initializes a new instance of the SlangVar class from JSON reflection data.
    /// </summary>
    /// <param name="reader">JSON object containing variable information</param>
    internal SlangVar(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Type = new(reader["type"]!.AsObject());
        UserAttributes = reader.ContainsKey("userAttribs") ? [.. reader["userAttribs"]!.AsArray().Select(static reader => new SlangUserAttribute(reader!.AsObject()))] : [];
        Bindings = reader.ContainsKey("bindings") ? [.. reader["bindings"]!.AsArray().Select(static reader => new SlangBinding(reader!.AsObject()))] : reader.ContainsKey("binding") ? [new(reader["binding"]!.AsObject())] : [];
        Shared = reader["shared"].Deserialize<bool>();
        Stage = reader["stage"].Deserialize<SlangStage>();
        SemanticName = reader["semanticName"].Deserialize<string>();
        SemanticIndex = reader["semanticIndex"].Deserialize<uint>();
        Format = reader["format"].Deserialize<string>();
    }

    /// <summary>
    /// Gets the name of the variable.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the reflected variable type.
    /// </summary>
    public SlangType Type { get; }

    /// <summary>
    /// Gets the user-defined attributes associated with this variable.
    /// </summary>
    public SlangUserAttribute[] UserAttributes { get; }

    /// <summary>
    /// Gets all binding points associated with this variable.
    /// </summary>
    public SlangBinding[] Bindings { get; }

    /// <summary>
    /// Gets a value indicating whether the variable is shared.
    /// </summary>
    public bool Shared { get; }

    /// <summary>
    /// Gets the shader stage associated with this variable, when applicable.
    /// </summary>
    public SlangStage Stage { get; }

    /// <summary>
    /// Gets the semantic name associated with this variable, when applicable.
    /// </summary>
    public string? SemanticName { get; }

    /// <summary>
    /// Gets the semantic index associated with this variable.
    /// </summary>
    public uint SemanticIndex { get; }

    /// <summary>
    /// Gets the image format associated with this variable, when available.
    /// </summary>
    public string? Format { get; }
}
