namespace Slangc.NET;

/// <summary>
/// Specifies the shader stage for entry points in the graphics or compute pipeline.
/// </summary>
public enum SlangStage
{
    /// <summary>
    /// Unknown or unspecified shader stage.
    /// </summary>
    Unknown,

    /// <summary>
    /// Vertex shader stage - processes individual vertices.
    /// </summary>
    Vertex,

    /// <summary>
    /// Hull shader stage (tessellation control) - controls tessellation.
    /// </summary>
    Hull,

    /// <summary>
    /// Domain shader stage (tessellation evaluation) - evaluates tessellated vertices.
    /// </summary>
    Domain,

    /// <summary>
    /// Geometry shader stage - processes primitives and can generate new geometry.
    /// </summary>
    Geometry,

    /// <summary>
    /// Fragment shader stage (pixel shader) - processes individual pixels.
    /// </summary>
    Fragment,

    /// <summary>
    /// Compute shader stage - general purpose parallel computation.
    /// </summary>
    Compute,

    /// <summary>
    /// Mesh shader stage - processes mesh primitives and can generate new geometry.
    /// </summary>
    Mesh,

    /// <summary>
    /// Amplification shader stage - used in advanced rendering techniques to control mesh generation and amplification.
    /// </summary>
    Amplification
}
