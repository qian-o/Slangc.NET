using System.Text.Json.Serialization;

namespace Slangc.NET;

/// <summary>
/// Specifies the scalar data types available in shaders.
/// </summary>
public enum SlangScalarType
{
    /// <summary>
    /// Unknown or unspecified scalar type.
    /// </summary>
    Unknown,

    /// <summary>
    /// Void type (no value).
    /// </summary>
    Void,

    /// <summary>
    /// Boolean type (true or false).
    /// </summary>
    Bool,

    /// <summary>
    /// 8-bit signed integer.
    /// </summary>
    Int8,

    /// <summary>
    /// 8-bit unsigned integer.
    /// </summary>
    UInt8,

    /// <summary>
    /// 16-bit signed integer.
    /// </summary>
    Int16,

    /// <summary>
    /// 16-bit unsigned integer.
    /// </summary>
    UInt16,

    /// <summary>
    /// 32-bit signed integer.
    /// </summary>
    Int32,

    /// <summary>
    /// 32-bit unsigned integer.
    /// </summary>
    UInt32,

    /// <summary>
    /// 64-bit signed integer.
    /// </summary>
    Int64,

    /// <summary>
    /// 64-bit unsigned integer.
    /// </summary>
    UInt64,

    /// <summary>
    /// 16-bit floating point number (half precision).
    /// </summary>
    Float16,

    /// <summary>
    /// 32-bit floating point number (single precision).
    /// </summary>
    Float32,

    /// <summary>
    /// 64-bit floating point number (double precision).
    /// </summary>
    Float64,

    /// <summary>
    /// Signed integer with the native pointer width.
    /// </summary>
    IntPtr,

    /// <summary>
    /// Unsigned integer with the native pointer width.
    /// </summary>
    UIntPtr,

    /// <summary>
    /// 16-bit brain floating point number.
    /// </summary>
    BFloat16,

    /// <summary>
    /// 8-bit floating point number with four exponent bits and three mantissa bits.
    /// </summary>
    [JsonStringEnumMemberName("float_e4m3")]
    FloatE4M3,

    /// <summary>
    /// 8-bit floating point number with five exponent bits and two mantissa bits.
    /// </summary>
    [JsonStringEnumMemberName("float_e5m2")]
    FloatE5M2
}
