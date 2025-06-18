using System.Text.Json.Nodes;

namespace Slangc.NET;

public class SlangNamedTypeBinding
{
    internal SlangNamedTypeBinding(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Binding = new(reader["binding"]!.AsObject());
    }

    public string Name { get; }

    public SlangBinding Binding { get; }
}
