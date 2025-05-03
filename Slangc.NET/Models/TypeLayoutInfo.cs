using System.Text.Json.Nodes;

namespace Slangc.NET.Models;

/// <summary>
/// slang-reflection-json.cpp
/// static void emitReflectionTypeLayoutInfoJSON(PrettyWriter& writer, slang::TypeLayoutReflection* typeLayout)
/// </summary>
public class TypeLayoutInfo : TypeInfo
{
    internal TypeLayoutInfo(JsonObject reader) : base(reader)
    {
    }
}
