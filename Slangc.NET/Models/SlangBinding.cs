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

        if (Kind is SlangParameterCategory.Uniform)
        {
            Offset = reader["offset"].DeserializeSize();
            Size = reader["size"].DeserializeSize();
            ElementStride = reader["elementStride"].DeserializeSize();
        }
        else
        {
            Space = reader.ContainsKey("space") ? reader["space"].DeserializeSize() : 0;
            Index = reader["index"].DeserializeSize();
            Count = reader.ContainsKey("count") ? reader["count"].DeserializeSize() : 1;
        }

        Used = reader.ContainsKey("used") ? reader["used"].Deserialize<bool>() : null;
    }

    /// <summary>
    /// Gets the category of this binding (e.g., constant buffer, texture, sampler).
    /// </summary>
    public SlangParameterCategory Kind { get; }

    /// <summary>
    /// Gets the offset within uniform data, or <c>null</c> when not applicable. A value of -1 is unbounded; -2 is unknown.
    /// </summary>
    public long? Offset { get; }

    /// <summary>
    /// Gets the size of uniform data, or <c>null</c> when not applicable. A value of -1 is unbounded; -2 is unknown.
    /// </summary>
    public long? Size { get; }

    /// <summary>
    /// Gets the stride between uniform elements, or <c>null</c> when not applicable. A value of -1 is unbounded; -2 is unknown.
    /// </summary>
    public long? ElementStride { get; }

    /// <summary>
    /// Gets the binding space, or <c>null</c> for uniform data. A value of -1 is unbounded; -2 is unknown.
    /// </summary>
    public long? Space { get; }

    /// <summary>
    /// Gets the binding index, or <c>null</c> when not applicable. A value of -1 is unbounded; -2 is unknown.
    /// </summary>
    public long? Index { get; }

    /// <summary>
    /// Gets the number of bound elements, or <c>null</c> for uniform data. A value of -1 is unbounded; -2 is unknown.
    /// </summary>
    public long? Count { get; }

    /// <summary>
    /// Gets whether this binding is used by the shader, or <c>null</c> when Slang did not emit usage information.
    /// </summary>
    public bool? Used { get; }
}
