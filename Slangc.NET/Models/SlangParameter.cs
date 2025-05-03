using System.Text.Json.Nodes;

namespace Slangc.NET.Models;

public class SlangParameter
{
    internal SlangParameter(JsonObject reader)
    {
    }

    public string Name { get; }

    public SlangBinding Binding { get; }

    public SlangType Type { get; }
}
