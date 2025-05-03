using System.Text.Json.Nodes;
using Slangc.NET.Enums;

namespace Slangc.NET.Models;

/// <summary>
/// slang-reflection-json.cpp
/// static void emitReflectionTypeLayoutInfoJSON(PrettyWriter& writer, slang::TypeLayoutReflection* typeLayout)
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
        ResultType = reader.ContainsKey("resultType") ? new(reader["resultType"]!.AsObject()) : null;
        ElementType = reader.ContainsKey("elementType") ? new(reader["elementType"]!.AsObject()) : null;
        ScalarType = reader["scalarType"].Deserialize<SlangScalarType>();
        ElementCount = reader["elementCount"].Deserialize<uint>();
        UniformStride = reader["uniformStride"].Deserialize<uint>();
        RowCount = reader["rowCount"].Deserialize<uint>();
        ColumnCount = reader["columnCount"].Deserialize<uint>();
        TargetType = reader.ContainsKey("targetType") ? new(reader["targetType"]!.AsObject()) : null;
        ValueType = reader["valueType"]?.Deserialize<string>();
        Fields = reader.ContainsKey("fields") ? reader["fields"]!.AsArray() : [];
        Name = reader["name"]?.Deserialize<string>();
        ContainerVarLayout = reader["containerVarLayout"].Deserialize<SlangBinding>();
        ElementVarLayout = reader.ContainsKey("elementVarLayout") ? reader["elementVarLayout"]!.AsObject() : null;
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

    #region Kind is ConstantBuffer or ParameterBlock or TextureBuffer or ShaderStorageBuffer or Vector or Matrix or Array
    public SlangType? ElementType { get; }
    #endregion

    #region Kind is Scalar
    public SlangScalarType ScalarType { get; }
    #endregion

    #region Kind is Vector or Array
    public uint ElementCount { get; }

    public uint UniformStride { get; }
    #endregion

    #region Kind is Matrix
    public uint RowCount { get; }

    public uint ColumnCount { get; }
    #endregion

    #region Kind is Pointer
    public SlangType? TargetType { get; }

    public string? ValueType { get; }
    #endregion

    #region Kind is Struct
    public JsonArray Fields { get; }
    #endregion

    #region Kind is GenericTypeParameter or Interface or Feedback
    public string? Name { get; }
    #endregion

    #region Kind is ConstantBuffer or ParameterBlock or TextureBuffer
    public SlangBinding ContainerVarLayout { get; }

    public JsonObject? ElementVarLayout { get; }
    #endregion
}
