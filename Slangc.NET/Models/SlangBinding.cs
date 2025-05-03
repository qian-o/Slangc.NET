using System.Text.Json.Nodes;
using Slangc.NET.Enums;

namespace Slangc.NET.Models;

public class SlangBinding
{
    internal SlangBinding(JsonNode json)
    {
    }

    public SlangParameterCategory Kind { get; }

    public uint Space { get; }

    public uint Index { get; }

    public bool Used { get; }
}
