namespace Slangc.NET;

public enum SlangStage : uint
{
    None,

    Vertex,

    Hull,

    Domain,

    Geometry,

    Pixel,

    Compute,

    RayGeneration,

    Intersection,

    AnyHit,

    ClosestHit,

    Miss,

    Callable,

    Mesh,

    Amplification,

    Count
};