using System.Text.Json.Nodes;

namespace Slangc.NET.Models;

public class SlangParameter
{
    internal SlangParameter(JsonNode json)
    {
    }

    public string Name { get; }

    public SlangBinding Binding { get; }

    public SlangType Type { get; }
}
