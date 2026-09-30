using System.Buffers;
using System.Text;

namespace Slangc.NET;

/// <summary>
/// Represents a null-terminated UTF-8 encoded string backed by an ArrayPool buffer.
/// Used to efficiently convert .NET strings to UTF-8 byte sequences for native API interop.
/// </summary>
internal readonly struct Utf8String : IDisposable
{
    /// <summary>
    /// The byte array containing the null-terminated UTF-8 encoded data.
    /// </summary>
    public readonly byte[] Data;

    /// <summary>
    /// Encodes a single string as a null-terminated UTF-8 byte sequence.
    /// </summary>
    /// <param name="str">The string to encode.</param>
    public Utf8String(string str)
    {
        Data = ArrayPool<byte>.Shared.Rent(Encoding.UTF8.GetByteCount(str) + 1);
        Data[Encoding.UTF8.GetBytes(str, Data)] = 0;
    }

    /// <summary>
    /// Encodes multiple strings as consecutive null-terminated UTF-8 byte sequences,
    /// recording the byte offset of each string in <paramref name="offsets"/>.
    /// </summary>
    /// <param name="strings">The strings to encode.</param>
    /// <param name="offsets">Receives the starting byte offset of each string in <see cref="Data"/>.</param>
    public Utf8String(ReadOnlySpan<string> strings, Span<int> offsets)
    {
        int length = 0;
        for (int i = 0; i < strings.Length; i++)
        {
            length += Encoding.UTF8.GetByteCount(strings[i]) + 1;
        }

        Data = ArrayPool<byte>.Shared.Rent(length);

        int position = 0;
        for (int i = 0; i < strings.Length; i++)
        {
            position += Encoding.UTF8.GetBytes(strings[i], Data.AsSpan(offsets[i] = position));
            Data[position++] = 0;
        }
    }

    /// <summary>
    /// Returns the byte array back to the ArrayPool.
    /// </summary>
    public void Dispose()
    {
        ArrayPool<byte>.Shared.Return(Data);
    }
}
