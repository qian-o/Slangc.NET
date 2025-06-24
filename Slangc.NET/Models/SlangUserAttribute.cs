using System.Text.Json;
using System.Text.Json.Nodes;

namespace Slangc.NET;

/// <summary>
/// Represents a user-defined attribute that can be attached to shader parameters or other elements.
/// </summary>
public class SlangUserAttribute
{
    /// <summary>
    /// Represents an argument value for a user attribute that can be either a number or string.
    /// </summary>
    public class Argument(JsonValue value)
    {
        /// <summary>
        /// Gets the numeric value of the argument if it's a number, otherwise returns 0.0.
        /// </summary>
        public double NumberValue { get; } = value.GetValueKind() is JsonValueKind.Number ? value.Deserialize<double>() : 0.0;

        /// <summary>
        /// Gets the string value of the argument if it's a string, otherwise returns an empty string.
        /// </summary>
        public string StringValue { get; } = value.GetValueKind() is JsonValueKind.String ? value.Deserialize<string>() : string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the SlangUserAttribute class from JSON reflection data.
    /// </summary>
    /// <param name="reader">JSON object containing user attribute information</param>
    internal SlangUserAttribute(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Arguments = [.. reader["arguments"]!.AsArray().Select(static value => new Argument(value!.AsValue()))];
    }

    /// <summary>
    /// Gets the name of the user attribute.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the array of arguments passed to the user attribute.
    /// </summary>
    public Argument[] Arguments { get; }
}
