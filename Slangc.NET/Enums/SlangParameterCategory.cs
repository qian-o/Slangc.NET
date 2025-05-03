namespace Slangc.NET.Enums;

/// <summary>
/// https://github.com/shader-slang/slang/blob/master/source/slang/slang-reflection-json.cpp
/// static void emitReflectionVarBindingInfoJSON(PrettyWriter& writer, SlangParameterCategory category, SlangUInt index, SlangUInt count, SlangUInt space = 0)
/// </summary>
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
