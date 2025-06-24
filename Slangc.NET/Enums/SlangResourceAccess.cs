namespace Slangc.NET;

/// <summary>
/// Specifies the access pattern for shader resources.
/// </summary>
public enum SlangResourceAccess
{
    /// <summary>
    /// Unknown or unspecified access pattern.
    /// </summary>
    Unknown,

    /// <summary>
    /// Read-only access to the resource.
    /// </summary>
    Read,

    /// <summary>
    /// Write-only access to the resource.
    /// </summary>
    Write,

    /// <summary>
    /// Read-write access to the resource.
    /// </summary>
    ReadWrite,

    /// <summary>
    /// Raster-ordered access for pixel shaders.
    /// </summary>
    RasterOrdered,

    /// <summary>
    /// Append access for structured buffers.
    /// </summary>
    Append,

    /// <summary>
    /// Consume access for structured buffers.
    /// </summary>
    Consume,

    /// <summary>
    /// Feedback access for raytracing.
    /// </summary>
    Feedback
}
