using System.Text.Json.Nodes;

namespace Slangc.NET.Models;

/// <summary>
/// https://github.com/shader-slang/slang/blob/master/source/slang/slang-reflection-json.cpp
/// static void emitReflectionVarInfoJSON(PrettyWriter& writer, slang::VariableReflection* var)
/// </summary>
public class SlangVar
{
    internal SlangVar(JsonObject reader)
    {
    }
}
