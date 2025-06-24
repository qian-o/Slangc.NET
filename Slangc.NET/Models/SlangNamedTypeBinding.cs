using System.Text.Json.Nodes;

namespace Slangc.NET;

/// <summary>
/// Represents a named type with its associated binding information.
/// </summary>
public class SlangNamedTypeBinding
{
    /// <summary>
    /// Initializes a new instance of the SlangNamedTypeBinding class from JSON reflection data.
    /// </summary>
    /// <param name="reader">JSON object containing named type binding information</param>
    internal SlangNamedTypeBinding(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Binding = new(reader["binding"]!.AsObject());
    }

    /// <summary>
    /// Gets the name of the type binding.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the binding information for this named type.
    /// </summary>
    public SlangBinding Binding { get; }
}
