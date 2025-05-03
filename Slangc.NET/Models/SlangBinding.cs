using System.Text.Json;
using System.Text.Json.Nodes;
using Slangc.NET.Enums;

namespace Slangc.NET.Models;

/// <summary>
/// slang-reflection-json.cpp
/// static void emitReflectionVarBindingInfoJSON(PrettyWriter& writer, SlangParameterCategory category, SlangUInt index, SlangUInt count, SlangUInt space = 0)
/// </summary>
public class SlangBinding
{
    internal SlangBinding(JsonObject reader)
    {
        Kind = reader["kind"].Deserialize<SlangParameterCategory>();
        Offset = reader["offset"].Deserialize<uint>();
        Size = reader["size"].Deserialize<uint>();
        Space = reader["space"].Deserialize<uint>();
        Index = reader["index"].Deserialize<uint>();
        Count = reader.ContainsKey("count") ? reader["count"]!.GetValueKind() is JsonValueKind.String ? uint.MaxValue : reader["count"].Deserialize<uint>() : 0;
        Used = reader["used"].Deserialize<bool>();
    }

    public SlangParameterCategory Kind { get; }

    public uint Offset { get; }

    public uint Size { get; }

    public uint Space { get; }

    public uint Index { get; }

    public uint Count { get; }

    public bool Used { get; }
}
