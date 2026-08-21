namespace Slangc.NET;

/// <summary>
/// Specifies the category of a shader parameter, determining how it should be bound to the graphics pipeline.
/// </summary>
public enum SlangParameterCategory
{
    /// <summary>
    /// Unknown or unspecified parameter category.
    /// </summary>
    Unknown,

    /// <summary>
    /// Constant buffer (uniform buffer) containing shader constants.
    /// </summary>
    ConstantBuffer,

    /// <summary>
    /// Shader resource view (texture, buffer) for read-only access.
    /// </summary>
    ShaderResource,

    /// <summary>
    /// Unordered access view for read-write access.
    /// </summary>
    UnorderedAccess,

    /// <summary>
    /// Varying input from previous pipeline stage.
    /// </summary>
    VaryingInput,

    /// <summary>
    /// Varying output to next pipeline stage.
    /// </summary>
    VaryingOutput,

    /// <summary>
    /// Sampler state for texture sampling.
    /// </summary>
    SamplerState,

    /// <summary>
    /// Uniform parameter (OpenGL-style uniform).
    /// </summary>
    Uniform,

    /// <summary>
    /// Push constant buffer for small amounts of frequently updated data.
    /// </summary>
    PushConstantBuffer,

    /// <summary>
    /// Descriptor table slot for bindless resources.
    /// </summary>
    DescriptorTableSlot,

    /// <summary>
    /// Specialization constant for compile-time constants.
    /// </summary>
    SpecializationConstant,

    /// <summary>
    /// Mixed parameter containing multiple categories.
    /// </summary>
    Mixed,

    /// <summary>
    /// Register space for parameter organization.
    /// </summary>
    RegisterSpace,

    /// <summary>
    /// Sub-element register space.
    /// </summary>
    SubElementRegisterSpace,

    /// <summary>
    /// Generic parameter category.
    /// </summary>
    Generic,

    /// <summary>
    /// Metal argument buffer element.
    /// </summary>
    MetalArgumentBufferElement,

    /// <summary>
    /// Ray payload data.
    /// </summary>
    RayPayload,

    /// <summary>
    /// Ray-tracing hit attributes.
    /// </summary>
    HitAttributes,

    /// <summary>
    /// Callable shader payload data.
    /// </summary>
    CallablePayload,

    /// <summary>
    /// Shader record data.
    /// </summary>
    ShaderRecord,

    /// <summary>
    /// Existential type parameter data.
    /// </summary>
    ExistentialTypeParam,

    /// <summary>
    /// Existential object parameter data.
    /// </summary>
    ExistentialObjectParam,

    /// <summary>
    /// Input attachment index.
    /// </summary>
    InputAttachmentIndex,

    /// <summary>
    /// Metal vertex attribute.
    /// </summary>
    MetalAttribute,

    /// <summary>
    /// Metal payload data.
    /// </summary>
    MetalPayload
}
