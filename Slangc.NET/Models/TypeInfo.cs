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

    #region Kind is Resource
    public SlangResourceShape BaseShape { get; }

    public bool Array { get; }

    public bool Multisample { get; }

    public bool Feedback { get; }

    public SlangResourceAccess Access { get; }

    public TypeInfo? ResultType { get; }
    #endregion

    #region Kind is ConstantBuffer or ParameterBlock or TextureBuffer or ShaderStorageBuffer or Vector or Matrix or Array
    public TypeInfo? ElementType { get; }
    #endregion

    #region Kind is Scalar
    public SlangScalarType ScalarType { get; }
    #endregion

    #region Kind is Vector or Array
    public uint ElementCount { get; }
    #endregion

    #region Kind is Matrix
    public uint RowCount { get; }

    public uint ColumnCount { get; }
    #endregion

    #region Kind is Pointer
    public TypeInfo? TargetType { get; }
    #endregion

    #region Kind is Struct
    public VarInfo[]? Fields { get; }
    #endregion

    #region Kind is GenericTypeParameter or Interface or Feedback
    public string? Name { get; }
    #endregion
}
