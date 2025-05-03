namespace Slangc.NET.Enums;

/// <summary>
/// https://github.com/shader-slang/slang/blob/master/source/slang/slang-reflection-json.cpp#L508
/// static void emitReflectionTypeInfoJSON(PrettyWriter& writer, slang::TypeReflection* type)
/// </summary>
public enum SlangTypeKind
{
    Unknown,

    SamplerState,

    Resource,

    ConstantBuffer,

    ParameterBlock,

    TextureBuffer,

    ShaderStorageBuffer,

    Scalar,

    Vector,

    Matrix,

    Array,

    Pointer,

    Struct,

    GenericTypeParameter,

    Interface,

    Feedback,

    DynamicResource
}
