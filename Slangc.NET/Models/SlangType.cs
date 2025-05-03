using System.Text.Json.Nodes;
using Slangc.NET.Enums;

namespace Slangc.NET.Models;

/// <summary>
/// https://github.com/shader-slang/slang/blob/master/source/slang/slang-reflection-json.cpp
/// static void emitReflectionTypeInfoJSON(PrettyWriter& writer, slang::TypeReflection* type)
/// </summary>
public class SlangType
{
    internal SlangType(JsonObject reader)
    {
        Kind = reader["kind"].Deserialize<SlangTypeKind>();
        BaseShape = reader["baseShape"].Deserialize<SlangResourceShape>();
        Array = reader["array"].Deserialize<bool>();
        Multisample = reader["multisample"].Deserialize<bool>();
        Feedback = reader["feedback"].Deserialize<bool>();
        Access = reader["access"].Deserialize<SlangResourceAccess>();
        ResultType = reader.ContainsKey("resultType") ? new SlangType(reader["resultType"]!.AsObject()) : null;
        ElementType = reader.ContainsKey("elementType") ? new SlangType(reader["elementType"]!.AsObject()) : null;
    }

    public SlangTypeKind Kind { get; }

    #region Kind is Resource
    public SlangResourceShape BaseShape { get; }

    public bool Array { get; }

    public bool Multisample { get; }

    public bool Feedback { get; }

    public SlangResourceAccess Access { get; }

    public SlangType? ResultType { get; }
    #endregion

    #region Kind is ConstantBuffer or ParameterBlock or TextureBuffer or ShaderStorageBuffer
    public SlangType? ElementType { get; }
    #endregion
}
