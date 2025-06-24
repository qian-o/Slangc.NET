namespace Slangc.NET;

/// <summary>
/// Specifies the kind of type represented by a SlangType instance.
/// </summary>
public enum SlangTypeKind
{
    /// <summary>
    /// Unknown or unspecified type.
    /// </summary>
    Unknown,

    /// <summary>
    /// Struct type with named fields.
    /// </summary>
    Struct,

    /// <summary>
    /// Array type with multiple elements of the same type.
    /// </summary>
    Array,

    /// <summary>
    /// Matrix type for linear algebra operations.
    /// </summary>
    Matrix,

    /// <summary>
    /// Vector type with multiple components.
    /// </summary>
    Vector,

    /// <summary>
    /// Scalar type (single value like int, float, bool).
    /// </summary>
    Scalar,

    /// <summary>
    /// Constant buffer type for shader constants.
    /// </summary>
    ConstantBuffer,

    /// <summary>
    /// Resource type (texture, buffer, etc.).
    /// </summary>
    Resource,

    /// <summary>
    /// Sampler state type for texture sampling.
    /// </summary>
    SamplerState,

    /// <summary>
    /// Texture buffer type for buffer-like textures.
    /// </summary>
    TextureBuffer,

    /// <summary>
    /// Shader storage buffer type for read-write structured data.
    /// </summary>
    ShaderStorageBuffer,

    /// <summary>
    /// Parameter block type for grouping related parameters.
    /// </summary>
    ParameterBlock,

    /// <summary>
    /// Generic type parameter for templated types.
    /// </summary>
    GenericTypeParameter,

    /// <summary>
    /// Interface type for dynamic dispatch.
    /// </summary>
    Interface,

    /// <summary>
    /// Feedback type for raytracing feedback.
    /// </summary>
    Feedback,

    /// <summary>
    /// Pointer type for indirection.
    /// </summary>
    Pointer,

    /// <summary>
    /// Dynamic resource type for bindless resources.
    /// </summary>
    DynamicResource
}
