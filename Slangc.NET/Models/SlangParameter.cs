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
        Binding = new(reader["binding"]!.AsObject());
        Type = new(reader["type"]!.AsObject());
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
    /// Gets the binding information for this parameter, including binding set and register numbers.
    /// </summary>
    public SlangBinding Binding { get; }

    /// <summary>
    /// Gets the type information for this parameter, including its structure and properties.
    /// </summary>
    public SlangType Type { get; }
}
