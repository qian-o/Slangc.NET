using System.Text.Json.Nodes;

namespace Slangc.NET.Models;

public class SlangParameterBinding
{
    internal SlangParameterBinding(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Binding = new(reader["binding"]!.AsObject());
    }

    public string Name { get; }

    public SlangBinding Binding { get; }
}
