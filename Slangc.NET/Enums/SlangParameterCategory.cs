namespace Slangc.NET.Enums;

public enum SlangParameterCategory
{
    None,

    Unknown = None,

    Mixed,

    ConstantBuffer,

    MetalBuffer = ConstantBuffer,

    ShaderResource,

    MetalTexture = ShaderResource,

    UnorderedAccess,

    VaryingInput,

    VertexInput = VaryingInput,

    VaryingOutput,

    FragmentOutput = VaryingOutput,

    SamplerState,

    MetalSampler = SamplerState,

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

    CountV1 = Subpass,

    MetalArgumentBufferElement,

    MetalAttribute,

    MetalPayload,

    Count
}
