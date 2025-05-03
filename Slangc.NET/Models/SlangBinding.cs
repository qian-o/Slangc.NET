using System.Text.Json.Nodes;
using Slangc.NET.Enums;

namespace Slangc.NET.Models;

public class SlangBinding
{
    internal SlangBinding(JsonObject reader)
    {
        Kind = reader["kind"].Deserialize<SlangParameterCategory>();
        Space = reader["space"].Deserialize<uint>();
        Index = reader["index"].Deserialize<uint>();
        Used = reader["used"].Deserialize<bool>();
    }

    public SlangParameterCategory Kind { get; }

    public uint Space { get; }

    public uint Index { get; }

    public bool Used { get; }
}
