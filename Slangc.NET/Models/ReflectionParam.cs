using System.Text.Json.Nodes;

namespace Slangc.NET.Models;

/// <summary>
/// https://github.com/shader-slang/slang/blob/master/source/slang/slang-reflection-json.cpp
/// static void emitReflectionParamJSON(PrettyWriter& writer, slang::VariableLayoutReflection* param)
/// </summary>
public class ReflectionParam
{
    internal ReflectionParam(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Binding = new(reader["binding"]!.AsObject());
    }

    public string Name { get; }

    public VarBindingInfo Binding { get; }
}
