namespace Slangc.NET.Enums;

/// <summary>
/// https://github.com/shader-slang/slang/blob/master/source/slang/slang-reflection-json.cpp#L423
/// static void emitReflectionResourceTypeBaseInfoJSON(PrettyWriter& writer, slang::TypeReflection* type)
/// </summary>
public enum SlangResourceShape
{
    Unknown,

    Texture1D,

    Texture2D,

    Texture3D,

    TextureCube,

    TextureBuffer,

    StructuredBuffer,

    ByteAddressBuffer,

    AccelerationStructure
}
