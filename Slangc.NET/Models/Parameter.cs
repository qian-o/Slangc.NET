using System.Text.Json.Nodes;

namespace Slangc.NET.Models;

/// <summary>
/// slang-reflection-json.cpp
/// static void emitReflectionParamJSON(PrettyWriter& writer, slang::VariableLayoutReflection* param)
/// </summary>
public class Parameter
{
    internal Parameter(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Binding = new(reader["binding"]!.AsObject());
    }

    public string Name { get; }

    public VarBindingInfo Binding { get; }
}
