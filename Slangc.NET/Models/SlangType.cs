using System.Text.Json.Nodes;

namespace Slangc.NET;

/// <summary>
/// Represents type information for shader variables, including complex types like structs, arrays, matrices, and resource types.
/// </summary>
public class SlangType
{
    /// <summary>
    /// Properties for struct types, containing the list of fields.
    /// </summary>
    public class StructProperties(JsonObject reader)
    {
        /// <summary>
        /// Gets all fields defined in the struct.
        /// </summary>
        public SlangVar[] Fields { get; } = [.. reader["fields"]!.AsArray().Select(static reader => new SlangVar(reader!.AsObject()))];
    }

    /// <summary>
    /// Properties for array types, including element count, stride, and element type.
    /// </summary>
    public class ArrayProperties(JsonObject reader)
    {
        /// <summary>
        /// Gets the number of elements in the array.
        /// </summary>
        public uint ElementCount { get; } = reader["elementCount"].Deserialize<uint>();

        /// <summary>
        /// Gets the stride (in bytes) of each element in the uniform buffer layout.
        /// </summary>
        public uint UniformStride { get; } = reader["uniformStride"].Deserialize<uint>();

        /// <summary>
        /// Gets the type information for array elements.
        /// </summary>
        public SlangType ElementType { get; } = new(reader["elementType"]!.AsObject());
    }

    /// <summary>
    /// Properties for matrix types, including row/column counts and element type.
    /// </summary>
    public class MatrixProperties(JsonObject reader)
    {
        /// <summary>
        /// Gets the number of rows in the matrix.
        /// </summary>
        public uint RowCount { get; } = reader["rowCount"].Deserialize<uint>();

        /// <summary>
        /// Gets the number of columns in the matrix.
        /// </summary>
        public uint ColumnCount { get; } = reader["columnCount"].Deserialize<uint>();

        /// <summary>
        /// Gets the scalar type information for matrix elements.
        /// </summary>
        public SlangType ElementType { get; } = new(reader["elementType"]!.AsObject());
    }

    /// <summary>
    /// Properties for vector types, including component count, stride, and element type.
    /// </summary>
    public class VectorProperties(JsonObject reader)
    {
        /// <summary>
        /// Gets the number of components in the vector.
        /// </summary>
        public uint ElementCount { get; } = reader["elementCount"].Deserialize<uint>();

        /// <summary>
        /// Gets the stride (in bytes) of the vector in the uniform buffer layout.
        /// </summary>
        public uint UniformStride { get; } = reader["uniformStride"].Deserialize<uint>();

        /// <summary>
        /// Gets the scalar type information for vector components.
        /// </summary>
        public SlangType ElementType { get; } = new(reader["elementType"]!.AsObject());
    }

    /// <summary>
    /// Properties for scalar types, specifying the concrete scalar data type.
    /// </summary>
    public class ScalarProperties(JsonObject reader)
    {
        /// <summary>
        /// Gets the scalar data type (e.g., Float32, Int32).
        /// </summary>
        public SlangScalarType ScalarType { get; } = reader["scalarType"].Deserialize<SlangScalarType>();
    }

    /// <summary>
    /// Properties for constant buffer types, including element type and layout information.
    /// </summary>
    public class ConstantBufferProperties(JsonObject reader)
    {
        /// <summary>
        /// Gets the element type contained within the constant buffer.
        /// </summary>
        public SlangType ElementType { get; } = new(reader["elementType"]!.AsObject());

        /// <summary>
        /// Gets the binding layout information for the constant buffer container itself.
        /// </summary>
        public SlangBinding ContainerVarLayout { get; } = new(reader["containerVarLayout"]!.AsObject());

        /// <summary>
        /// Gets the variable layout information for elements inside the constant buffer.
        /// </summary>
        public SlangVar ElementVarLayout { get; } = new(reader["elementVarLayout"]!.AsObject());
    }

    /// <summary>
    /// Properties for resource types, including shape, access mode, and multisampling information.
    /// </summary>
    public class ResourceProperties(JsonObject reader)
    {
        /// <summary>
        /// Gets the base shape of the resource (e.g., Texture2D, StructuredBuffer).
        /// </summary>
        public SlangResourceShape BaseShape { get; } = reader["baseShape"].Deserialize<SlangResourceShape>();

        /// <summary>
        /// Gets a value indicating whether the resource is an array type.
        /// </summary>
        public bool Array { get; } = reader["array"].Deserialize<bool>();

        /// <summary>
        /// Gets a value indicating whether the resource supports multisampling.
        /// </summary>
        public bool Multisample { get; } = reader["multisample"].Deserialize<bool>();

        /// <summary>
        /// Gets a value indicating whether the resource is a feedback resource (used in raytracing).
        /// </summary>
        public bool Feedback { get; } = reader["feedback"].Deserialize<bool>();

        /// <summary>
        /// Gets a value indicating whether the resource is a combined texture-sampler.
        /// </summary>
        public bool Combined { get; } = reader["combined"].Deserialize<bool>();

        /// <summary>
        /// Gets the access mode of the resource (read-only, write-only, read-write, etc.).
        /// </summary>
        public SlangResourceAccess Access { get; } = reader["access"].Deserialize<SlangResourceAccess>();

        /// <summary>
        /// Gets the result type of a resource access (e.g., the data type returned by a texture sample).
        /// <c>null</c> if the resource has no result type.
        /// </summary>
        public SlangType? ResultType { get; } = reader.ContainsKey("resultType") ? new(reader["resultType"]!.AsObject()) : null;
    }

    /// <summary>
    /// Properties for texture buffer types, including element type and layout information.
    /// </summary>
    public class TextureBufferProperties(JsonObject reader)
    {
        /// <summary>
        /// Gets the element type contained within the texture buffer.
        /// </summary>
        public SlangType ElementType { get; } = new(reader["elementType"]!.AsObject());

        /// <summary>
        /// Gets the binding layout information for the texture buffer container itself.
        /// </summary>
        public SlangBinding ContainerVarLayout { get; } = new(reader["containerVarLayout"]!.AsObject());

        /// <summary>
        /// Gets the variable layout information for elements inside the texture buffer.
        /// </summary>
        public SlangVar ElementVarLayout { get; } = new(reader["elementVarLayout"]!.AsObject());
    }

    /// <summary>
    /// Properties for shader storage buffer types, including element type.
    /// </summary>
    public class ShaderStorageBufferProperties(JsonObject reader)
    {
        /// <summary>
        /// Gets the element type contained within the shader storage buffer.
        /// </summary>
        public SlangType ElementType { get; } = new(reader["elementType"]!.AsObject());
    }

    /// <summary>
    /// Properties for parameter block types, including element type and layout information.
    /// </summary>
    public class ParameterBlockProperties(JsonObject reader)
    {
        /// <summary>
        /// Gets the element type contained within the parameter block.
        /// </summary>
        public SlangType ElementType { get; } = new(reader["elementType"]!.AsObject());

        /// <summary>
        /// Gets the binding layout information for the parameter block container itself.
        /// </summary>
        public SlangBinding ContainerVarLayout { get; } = new(reader["containerVarLayout"]!.AsObject());

        /// <summary>
        /// Gets the variable layout information for elements inside the parameter block.
        /// </summary>
        public SlangVar ElementVarLayout { get; } = new(reader["elementVarLayout"]!.AsObject());
    }

    /// <summary>
    /// Properties for pointer types, containing the name of the pointed-to value type.
    /// </summary>
    public class PointerProperties(JsonObject reader)
    {
        /// <summary>
        /// Gets the name of the value type this pointer points to.
        /// </summary>
        public string ValueType { get; } = reader["valueType"]!.Deserialize<string>();
    }

    /// <summary>
    /// Properties for named types (generic type parameters, interfaces, and feedback types).
    /// </summary>
    public class NamedTypeProperties(JsonObject reader)
    {
        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public string Name { get; } = reader["name"].Deserialize<string>();
    }

    /// <summary>
    /// Initializes a new instance of <see cref="SlangType"/> from JSON reflection data,
    /// populating the corresponding properties based on the type kind.
    /// </summary>
    /// <param name="reader">JSON object containing the type information.</param>
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
                NamedType = new(reader);
                break;
        }
    }

    /// <summary>
    /// Gets the kind of this type.
    /// </summary>
    public SlangTypeKind Kind { get; }

    /// <summary>
    /// Gets the struct properties. Non-null when <see cref="Kind"/> is <see cref="SlangTypeKind.Struct"/>.
    /// </summary>
    public StructProperties? Struct { get; }

    /// <summary>
    /// Gets the array properties. Non-null when <see cref="Kind"/> is <see cref="SlangTypeKind.Array"/>.
    /// </summary>
    public ArrayProperties? Array { get; }

    /// <summary>
    /// Gets the matrix properties. Non-null when <see cref="Kind"/> is <see cref="SlangTypeKind.Matrix"/>.
    /// </summary>
    public MatrixProperties? Matrix { get; }

    /// <summary>
    /// Gets the vector properties. Non-null when <see cref="Kind"/> is <see cref="SlangTypeKind.Vector"/>.
    /// </summary>
    public VectorProperties? Vector { get; }

    /// <summary>
    /// Gets the scalar properties. Non-null when <see cref="Kind"/> is <see cref="SlangTypeKind.Scalar"/>.
    /// </summary>
    public ScalarProperties? Scalar { get; }

    /// <summary>
    /// Gets the constant buffer properties. Non-null when <see cref="Kind"/> is <see cref="SlangTypeKind.ConstantBuffer"/>.
    /// </summary>
    public ConstantBufferProperties? ConstantBuffer { get; }

    /// <summary>
    /// Gets the resource properties. Non-null when <see cref="Kind"/> is <see cref="SlangTypeKind.Resource"/>.
    /// </summary>
    public ResourceProperties? Resource { get; }

    /// <summary>
    /// Gets the texture buffer properties. Non-null when <see cref="Kind"/> is <see cref="SlangTypeKind.TextureBuffer"/>.
    /// </summary>
    public TextureBufferProperties? TextureBuffer { get; }

    /// <summary>
    /// Gets the shader storage buffer properties. Non-null when <see cref="Kind"/> is <see cref="SlangTypeKind.ShaderStorageBuffer"/>.
    /// </summary>
    public ShaderStorageBufferProperties? ShaderStorageBuffer { get; }

    /// <summary>
    /// Gets the parameter block properties. Non-null when <see cref="Kind"/> is <see cref="SlangTypeKind.ParameterBlock"/>.
    /// </summary>
    public ParameterBlockProperties? ParameterBlock { get; }

    /// <summary>
    /// Gets the pointer properties. Non-null when <see cref="Kind"/> is <see cref="SlangTypeKind.Pointer"/>.
    /// </summary>
    public PointerProperties? Pointer { get; }

    /// <summary>
    /// Gets the named type properties. Non-null when <see cref="Kind"/> is a generic type parameter, interface, or feedback type.
    /// </summary>
    public NamedTypeProperties? NamedType { get; }
}
