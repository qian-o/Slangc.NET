using System.Text.Json;
using System.Text.Json.Nodes;

namespace Slangc.NET;

public class SlangUserAttribute
{
    public class Argument(JsonValue value)
    {
        public double NumberValue { get; } = value.GetValueKind() is JsonValueKind.Number ? value.Deserialize<double>() : 0.0;

        public string StringValue { get; } = value.GetValueKind() is JsonValueKind.String ? value.Deserialize<string>() : string.Empty;
    }

    internal SlangUserAttribute(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Arguments = [.. reader["arguments"]!.AsArray().Select(static value => new Argument(value!.AsValue()))];
    }

    public string Name { get; }

    public Argument[] Arguments { get; }
}
