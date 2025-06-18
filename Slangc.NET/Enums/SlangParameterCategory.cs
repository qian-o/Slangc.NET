namespace Slangc.NET;

public enum SlangParameterCategory
{
    Unknown,

    ConstantBuffer,

    ShaderResource,

    UnorderedAccess,

    VaryingInput,

    VaryingOutput,

    SamplerState,

    Uniform,

    PushConstantBuffer,

    DescriptorTableSlot,

    SpecializationConstant,

    Mixed,

    RegisterSpace,

    SubElementRegisterSpace,

    Generic,

    MetalArgumentBufferElement
}
