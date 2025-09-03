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
        Bindings = reader.ContainsKey("bindings") ? [.. reader["bindings"]!.AsArray().Select(static reader => new SlangBinding(reader!.AsObject()))] : [new(reader["binding"]!.AsObject())];
    }

    /// <summary>
    /// Gets the name of the type binding.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the array of binding points for this named type, if it spans multiple bindings (e.g., arrays of resources).
    /// </summary>
    public SlangBinding[] Bindings { get; }
}
