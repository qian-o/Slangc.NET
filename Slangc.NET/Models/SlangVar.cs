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
        Binding = reader.ContainsKey("binding") ? new(reader["binding"]!.AsObject()) : null;
    }

    /// <summary>
    /// Gets the name of the variable.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the type information for this variable.
    /// </summary>
    public SlangType Type { get; }

    /// <summary>
    /// Gets the binding information for this variable, if it has one.
    /// May be null for variables that don't have explicit bindings.
    /// </summary>
    public SlangBinding? Binding { get; }
}
