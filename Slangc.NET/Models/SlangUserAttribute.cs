using System.Text.Json.Nodes;

namespace Slangc.NET;

public class SlangUserAttribute
{
    internal SlangUserAttribute(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Arguments = [.. reader["arguments"]!.AsArray().Select(static reader => reader.Deserialize<object>())];
    }

    public string Name { get; }

    public object[] Arguments { get; }
}
