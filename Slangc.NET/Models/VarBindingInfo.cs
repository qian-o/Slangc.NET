using System.Text.Json.Nodes;
using Slangc.NET.Enums;

namespace Slangc.NET.Models;

/// <summary>
/// https://github.com/shader-slang/slang/blob/master/source/slang/slang-reflection-json.cpp
/// static void emitReflectionVarBindingInfoJSON(PrettyWriter& writer, SlangParameterCategory category, SlangUInt index, SlangUInt count, SlangUInt space = 0)
/// </summary>
public class VarBindingInfo
{
    internal VarBindingInfo(JsonObject reader)
    {
        Kind = reader["kind"].Deserialize<SlangParameterCategory>();
        Offset = reader["offset"].Deserialize<uint>();
        Size = reader["size"].Deserialize<uint>();
        Space = reader["space"].Deserialize<uint>();
        Index = reader["index"].Deserialize<uint>();
        Used = reader["used"].Deserialize<bool>();
    }

    public SlangParameterCategory Kind { get; }

    public uint Offset { get; }

    public uint Size { get; }

    public uint Space { get; }

    public uint Index { get; }

    public bool Used { get; }
}
