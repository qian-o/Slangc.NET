using System.Text.Json.Nodes;

namespace Slangc.NET.Models;

public class SlangVar
{
    internal SlangVar(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Type = new(reader["type"]!.AsObject());
        Binding = reader.ContainsKey("binding") ? new(reader["binding"]!.AsObject()) : null;
    }

    public string Name { get; }

    public SlangType Type { get; }

    public SlangBinding? Binding { get; }
}
