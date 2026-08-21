namespace Slangc.NET;

/// <summary>
/// Describes how a shader scope's parameters are allocated.
/// </summary>
public enum SlangScopeKind
{
    /// <summary>
    /// Unknown or unsupported scope kind.
    /// </summary>
    Unknown,

    /// <summary>
    /// Parameters bind directly without an implicit container.
    /// </summary>
    None,

    /// <summary>
    /// Parameters are gathered into an automatically introduced constant buffer.
    /// </summary>
    ConstantBuffer
}