namespace Slangc.NET.Enums;

/// <summary>
/// https://github.com/shader-slang/slang/blob/master/source/slang/slang-reflection-json.cpp
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
