using System.Text.Json.Nodes;

namespace Slangc.NET;

public class SlangParameter
{
    internal SlangParameter(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        UserAttributes = reader.ContainsKey("userAttribs") ? [.. reader["userAttribs"]!.AsArray().Select(static reader => new SlangUserAttribute(reader!.AsObject()))] : [];
        Binding = new(reader["binding"]!.AsObject());
        Type = new(reader["type"]!.AsObject());
    }

    public string Name { get; }

    public SlangUserAttribute[] UserAttributes { get; }

    public SlangBinding Binding { get; }

    public SlangType Type { get; }
}
