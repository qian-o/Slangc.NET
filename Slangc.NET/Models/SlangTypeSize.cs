using System.Text.Json.Nodes;

namespace Slangc.NET;

/// <summary>
/// Represents the size and optional alignment of a type in one layout unit.
/// </summary>
public class SlangTypeSize
{
    /// <summary>
    /// Initializes type size information from Slang JSON reflection data.
    /// </summary>
    /// <param name="reader">JSON object containing type size information.</param>
    internal SlangTypeSize(JsonObject reader)
    {
        Kind = reader["kind"].Deserialize<SlangParameterCategory>();
        Value = reader["value"].DeserializeIntegerOrSentinel();

        if (reader.ContainsKey("alignment"))
        {
            Alignment = reader["alignment"].DeserializeIntegerOrSentinel();
        }
    }

    /// <summary>
    /// Gets the layout unit used by the size.
    /// </summary>
    public SlangParameterCategory Kind { get; }

    /// <summary>
    /// Gets the size value. A value of -1 is unbounded; -2 is unknown.
    /// </summary>
    public long Value { get; }

    /// <summary>
    /// Gets the alignment for this layout unit. A value of -1 is unbounded; -2 is unknown.
    /// </summary>
    public long Alignment { get; }
}