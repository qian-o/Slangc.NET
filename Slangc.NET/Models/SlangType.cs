using System.Text.Json.Nodes;
using Slangc.NET.Enums;

namespace Slangc.NET.Models;

public class SlangType
{
    public class StructProperties(JsonObject reader)
    {
        public SlangVar[] Fields { get; } = [.. reader["fields"]!.AsArray().Select(static item => new SlangVar(item!.AsObject()))];
    }

    public class ArrayProperties(JsonObject reader)
    {
        public uint ElementCount { get; } = reader["elementCount"].Deserialize<uint>();

        public uint UniformStride { get; } = reader["uniformStride"].Deserialize<uint>();

        public SlangType ElementType { get; } = new(reader["elementType"]!.AsObject());
    }

    public class MatrixProperties(JsonObject reader)
    {
        public uint RowCount { get; } = reader["rowCount"].Deserialize<uint>();

        public uint ColumnCount { get; } = reader["columnCount"].Deserialize<uint>();

        public SlangType ElementType { get; } = new(reader["elementType"]!.AsObject());
    }

    public class VectorProperties(JsonObject reader)
    {
        public uint ElementCount { get; } = reader["elementCount"].Deserialize<uint>();

        public uint UniformStride { get; } = reader["uniformStride"].Deserialize<uint>();

        public SlangType ElementType { get; } = new(reader["elementType"]!.AsObject());
    }

    public class ScalarProperties(JsonObject reader)
    {
        public SlangScalarType ScalarType { get; } = reader["scalarType"].Deserialize<SlangScalarType>();
    }

    public class ConstantBufferProperties(JsonObject reader)
    {
        public SlangType ElementType { get; } = new(reader["elementType"]!.AsObject());

        public SlangBinding ContainerVarLayout { get; } = new(reader["containerVarLayout"]!.AsObject());

        public object ElementVarLayout { get; } = reader["elementVarLayout"]!.AsObject();
    }

    public class ResourceProperties(JsonObject reader)
    {
        public SlangResourceShape BaseShape { get; } = reader["baseShape"].Deserialize<SlangResourceShape>();

        public bool Array { get; } = reader["array"].Deserialize<bool>();

        public bool Multisample { get; } = reader["multisample"].Deserialize<bool>();

        public bool Feedback { get; } = reader["feedback"].Deserialize<bool>();

        public SlangResourceAccess Access { get; } = reader["access"].Deserialize<SlangResourceAccess>();

        public SlangType ResultType { get; } = new(reader["resultType"]!.AsObject());
    }

    public class TextureBufferProperties(JsonObject reader)
    {
        public SlangType ElementType { get; } = new(reader["elementType"]!.AsObject());

        public SlangBinding ContainerVarLayout { get; } = new(reader["containerVarLayout"]!.AsObject());

        public object ElementVarLayout { get; } = reader["elementVarLayout"]!.AsObject();
    }

    public class ShaderStorageBufferProperties(JsonObject reader)
    {
        public SlangType ElementType { get; } = new(reader["elementType"]!.AsObject());
    }

    public class ParameterBlockProperties(JsonObject reader)
    {
        public SlangType ElementType { get; } = new(reader["elementType"]!.AsObject());

        public SlangBinding ContainerVarLayout { get; } = new(reader["containerVarLayout"]!.AsObject());

        public SlangVar ElementVarLayout { get; } = new(reader["elementVarLayout"]!.AsObject());
    }

    public class PointerProperties(JsonObject reader)
    {
        public SlangType TargetType { get; } = new(reader["targetType"]!.AsObject());

        public string ValueType { get; } = reader["valueType"].Deserialize<string>();
    }

    public class NamedTypeProperties(JsonObject reader)
    {
        public string Name { get; } = reader["name"].Deserialize<string>();
    }

    internal SlangType(JsonObject reader)
    {
        Kind = reader["kind"].Deserialize<SlangTypeKind>();

        switch (Kind)
        {
            case SlangTypeKind.Struct:
                Struct = new(reader);
                break;
            case SlangTypeKind.Array:
                Array = new(reader);
                break;
            case SlangTypeKind.Matrix:
                Matrix = new(reader);
                break;
            case SlangTypeKind.Vector:
                Vector = new(reader);
                break;
            case SlangTypeKind.Scalar:
                Scalar = new(reader);
                break;
            case SlangTypeKind.ConstantBuffer:
                ConstantBuffer = new(reader);
                break;
            case SlangTypeKind.Resource:
                Resource = new(reader);
                break;
            case SlangTypeKind.TextureBuffer:
                TextureBuffer = new(reader);
                break;
            case SlangTypeKind.ShaderStorageBuffer:
                ShaderStorageBuffer = new(reader);
                break;
            case SlangTypeKind.ParameterBlock:
                ParameterBlock = new(reader);
                break;
            case SlangTypeKind.Pointer:
                Pointer = new(reader);
                break;
            case SlangTypeKind.GenericTypeParameter:
            case SlangTypeKind.Interface:
            case SlangTypeKind.Feedback:
            case SlangTypeKind.DynamicResource:
                NamedType = new(reader);
                break;
        }
    }

    public SlangTypeKind Kind { get; }

    public StructProperties? Struct { get; }

    public ArrayProperties? Array { get; }

    public MatrixProperties? Matrix { get; }

    public VectorProperties? Vector { get; }

    public ScalarProperties? Scalar { get; }

    public ConstantBufferProperties? ConstantBuffer { get; }

    public ResourceProperties? Resource { get; }

    public TextureBufferProperties? TextureBuffer { get; }

    public ShaderStorageBufferProperties? ShaderStorageBuffer { get; }

    public ParameterBlockProperties? ParameterBlock { get; }

    public PointerProperties? Pointer { get; }

    public NamedTypeProperties? NamedType { get; }
}
