namespace Slangc.NET.Enums;

/// <summary>
/// slang.h
/// enum SlangParameterCategory : SlangParameterCategoryIntegral
/// </summary>
public enum SlangParameterCategory
{
    None,

    Mixed,

    ConstantBuffer,

    ShaderResource,

    UnorderedAccess,

    VaryingInput,

    VaryingOutput,

    SamplerState,

    Uniform,

    DescriptorTableSlot,

    SpecializationConstant,

    PushConstantBuffer,

    RegisterSpace,

    Generic,

    RayPayload,

    HitAttributes,

    CallablePayload,

    ShaderRecord,

    ExistentialTypeParam,

    ExistentialObjectParam,

    SubElementRegisterSpace,

    Subpass,

    MetalArgumentBufferElement,

    MetalAttribute,

    MetalPayload,

    Count,

    MetalBuffer,

    MetalTexture,

    MetalSampler,

    VertexInput,

    FragmentOutput,

    CountV1,

    Unknown
}
