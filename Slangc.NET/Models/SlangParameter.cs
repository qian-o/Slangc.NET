using System.Text.Json.Nodes;

namespace Slangc.NET.Models;

/// <summary>
/// slang-reflection-json.cpp
/// static void emitReflectionParamJSON(PrettyWriter& writer, slang::VariableLayoutReflection* param)
/// </summary>
public class SlangParameter
{
    internal SlangParameter(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Binding = new(reader["binding"]!.AsObject());
        Type = new(reader["type"]!.AsObject());
    }

    public string Name { get; }

    public SlangBinding Binding { get; }

    public SlangType Type { get; }
}
