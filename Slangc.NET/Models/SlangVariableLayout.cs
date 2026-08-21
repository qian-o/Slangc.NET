using System.Text.Json.Nodes;

namespace Slangc.NET;

/// <summary>
/// Represents layout metadata attached to a reflected variable.
/// </summary>
public class SlangVariableLayout
{
    /// <summary>
    /// Initializes variable layout information from JSON reflection data.
    /// </summary>
    /// <param name="reader">JSON object containing variable layout information.</param>
    internal SlangVariableLayout(JsonObject reader)
    {
        Bindings = reader.ContainsKey("bindings") ? [.. reader["bindings"]!.AsArray().Select(static reader => new SlangBinding(reader!.AsObject()))] : reader.ContainsKey("binding") ? [new(reader["binding"]!.AsObject())] : [];
        Stage = reader["stage"].Deserialize<SlangStage>();
        SemanticName = reader["semanticName"].Deserialize<string>();
        SemanticIndex = reader["semanticIndex"].Deserialize<uint>();
        Format = reader["format"].Deserialize<string>();
    }

    /// <summary>
    /// Gets all binding points associated with this layout.
    /// </summary>
    public SlangBinding[] Bindings { get; }

    /// <summary>
    /// Gets the shader stage associated with this layout, when applicable.
    /// </summary>
    public SlangStage Stage { get; }

    /// <summary>
    /// Gets the semantic name associated with this layout, when applicable.
    /// </summary>
    public string? SemanticName { get; }

    /// <summary>
    /// Gets the semantic index associated with this layout.
    /// </summary>
    public uint SemanticIndex { get; }

    /// <summary>
    /// Gets the image format associated with this layout, when available.
    /// </summary>
    public string? Format { get; }
}