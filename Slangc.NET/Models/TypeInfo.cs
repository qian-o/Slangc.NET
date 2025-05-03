using System.Text.Json.Nodes;
using Slangc.NET.Enums;

namespace Slangc.NET.Models;

/// <summary>
/// slang-reflection-json.cpp
/// static void emitReflectionTypeInfoJSON(PrettyWriter& writer, slang::TypeReflection* type)
/// </summary>
public class TypeInfo
{
    internal TypeInfo(JsonObject reader)
    {
        Kind = reader["kind"].Deserialize<SlangTypeKind>();
    }

    public SlangTypeKind Kind { get; }
}
