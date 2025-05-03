namespace Slangc.NET.Enums;

/// <summary>
/// https://github.com/shader-slang/slang/blob/master/include/slang.h
/// enum SlangResourceAccess : SlangResourceAccessIntegral
/// </summary>
public enum SlangResourceAccess
{
    Unknown,

    Read,

    ReadWrite,

    RasterOrdered,

    Append,

    Consume,

    Write,

    Feedback
}
