using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;

namespace Slangc.NET;

internal readonly struct TmpUtf8String(byte[] Data) : IDisposable
{
    public readonly byte[] Data = Data;

    public ref byte GetPinnableReference() => ref MemoryMarshal.GetArrayDataReference(Data);

    public void Dispose()
    {
        ArrayPool<byte>.Shared.Return(Data);
    }

    public static TmpUtf8String Alloc(string str)
    {
        var data = ArrayPool<byte>.Shared.Rent(Encoding.UTF8.GetMaxByteCount(str.Length) + 1);
        try
        {
            var count = Encoding.UTF8.GetBytes(str, data);
            data[count] = 0;
            return new(data);
        }
        catch
        {
            ArrayPool<byte>.Shared.Return(data);
            throw;
        }
    }

    public static TmpUtf8String Rent(int length) => new(ArrayPool<byte>.Shared.Rent(length));
}
