using System.Text.Json;
using System.Text.Json.Nodes;

namespace Slangc.NET;

/// <summary>
/// Represents binding information for a shader parameter, including its location, size, and usage details.
/// </summary>
public class SlangBinding
{
    /// <summary>
    /// Initializes a new instance of the SlangBinding class from JSON reflection data.
    /// </summary>
    /// <param name="reader">JSON object containing binding information</param>
    internal SlangBinding(JsonObject reader)
    {
        Kind = reader["kind"].Deserialize<SlangParameterCategory>();
        Offset = reader["offset"].Deserialize<uint>();
        Size = reader["size"].Deserialize<uint>();
        Space = reader["space"].Deserialize<uint>();
        Index = reader["index"].Deserialize<uint>();
        Count = reader.ContainsKey("count") ? reader["count"]!.GetValueKind() is JsonValueKind.String ? 0 : reader["count"].Deserialize<uint>() : 1;
        Used = !reader.ContainsKey("used") || reader["used"].Deserialize<bool>();
    }

    /// <summary>
    /// Gets the category of this binding (e.g., constant buffer, texture, sampler).
    /// </summary>
    public SlangParameterCategory Kind { get; }

    /// <summary>
    /// Gets the offset within the binding space.
    /// </summary>
    public uint Offset { get; }

    /// <summary>
    /// Gets the size of the binding in bytes.
    /// </summary>
    public uint Size { get; }

    /// <summary>
    /// Gets the binding space (register space in DirectX terminology).
    /// </summary>
    public uint Space { get; }

    /// <summary>
    /// Gets the binding index (register number in DirectX terminology).
    /// </summary>
    public uint Index { get; }

    /// <summary>
    /// Gets the number of elements in this binding (for arrays).
    /// </summary>
    public uint Count { get; }

    /// <summary>
    /// Gets a value indicating whether this binding is used by the shader.
    /// </summary>
    public bool Used { get; }
}
