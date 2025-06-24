namespace Slangc.NET;

/// <summary>
/// Specifies the shape or dimensionality of shader resources.
/// </summary>
public enum SlangResourceShape
{
    /// <summary>
    /// Unknown or unspecified resource shape.
    /// </summary>
    Unknown,

    /// <summary>
    /// 1D texture resource.
    /// </summary>
    Texture1D,

    /// <summary>
    /// 2D texture resource.
    /// </summary>
    Texture2D,

    /// <summary>
    /// 3D texture resource (volume texture).
    /// </summary>
    Texture3D,

    /// <summary>
    /// Cube texture resource (6 faces).
    /// </summary>
    TextureCube,

    /// <summary>
    /// Texture buffer (1D texture with buffer-like access).
    /// </summary>
    TextureBuffer,

    /// <summary>
    /// Structured buffer with typed elements.
    /// </summary>
    StructuredBuffer,

    /// <summary>
    /// Byte address buffer (raw buffer).
    /// </summary>
    ByteAddressBuffer,

    /// <summary>
    /// Acceleration structure for raytracing.
    /// </summary>
    AccelerationStructure
}
