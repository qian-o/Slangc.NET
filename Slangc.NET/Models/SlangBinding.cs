using System.Text.Json;
using System.Text.Json.Nodes;
using Slangc.NET.Enums;

namespace Slangc.NET.Models;

public class SlangBinding
{
    public const uint Unbounded = uint.MaxValue;

    internal SlangBinding(JsonObject reader)
    {
        Kind = reader["kind"].Deserialize<SlangParameterCategory>();
        Offset = reader["offset"].Deserialize<uint>();
        Size = reader["size"].Deserialize<uint>();
        Space = reader["space"].Deserialize<uint>();
        Index = reader["index"].Deserialize<uint>();
        Count = reader.ContainsKey("count") ? reader["count"]!.GetValueKind() is JsonValueKind.String ? Unbounded : reader["count"].Deserialize<uint>() : 1;
        Used = !reader.ContainsKey("used") || reader["used"].Deserialize<bool>();
    }

    public SlangParameterCategory Kind { get; }

    public uint Offset { get; }

    public uint Size { get; }

    public uint Space { get; }

    public uint Index { get; }

    public uint Count { get; }

    public bool Used { get; }
}
