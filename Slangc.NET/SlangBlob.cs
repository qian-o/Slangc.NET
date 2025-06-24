using System.Runtime.CompilerServices;

namespace Slangc.NET;

/// <summary>
/// Represents a native Slang blob that contains binary data with COM-like interface.
/// This structure provides access to buffer data returned by the Slang API.
/// </summary>
public unsafe struct SlangBlob
{
    /// <summary>
    /// Pointer to the virtual table for COM-like interface calls.
    /// </summary>
    public void** LpVtbl;

    /// <summary>
    /// Gets a pointer to the buffer data contained in this blob.
    /// </summary>
    /// <returns>Pointer to the buffer data</returns>
    public readonly void* GetBufferPointer()
    {
        SlangBlob* @this = (SlangBlob*)Unsafe.AsPointer(ref Unsafe.AsRef(in this));

        return ((delegate* unmanaged[Stdcall]<SlangBlob*, void*>)@this->LpVtbl[3])(@this);
    }

    /// <summary>
    /// Gets the size of the buffer data in bytes.
    /// </summary>
    /// <returns>Size of the buffer in bytes</returns>
    public readonly ulong GetBufferSize()
    {
        SlangBlob* @this = (SlangBlob*)Unsafe.AsPointer(ref Unsafe.AsRef(in this));

        return ((delegate* unmanaged[Stdcall]<SlangBlob*, ulong>)@this->LpVtbl[4])(@this);
    }
};
