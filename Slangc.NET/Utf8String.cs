using System.Buffers;
using System.Text;

namespace Slangc.NET;

internal readonly struct Utf8String : IDisposable
{
    public readonly byte[] Data;

    public Utf8String(string str)
    {
        Data = ArrayPool<byte>.Shared.Rent(Encoding.UTF8.GetByteCount(str) + 1);
        Data[Encoding.UTF8.GetBytes(str, Data)] = 0;
    }

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

    public void Dispose()
    {
        ArrayPool<byte>.Shared.Return(Data);
    }
}
