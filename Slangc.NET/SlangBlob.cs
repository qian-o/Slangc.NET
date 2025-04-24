using System.Runtime.CompilerServices;

namespace Slangc.NET;

public unsafe struct SlangBlob
{
    public void** LpVtbl;

    public readonly void* GetBufferPointer()
    {
        SlangBlob* @this = (SlangBlob*)Unsafe.AsPointer(ref Unsafe.AsRef(in this));

        return ((delegate* unmanaged[Stdcall]<SlangBlob*, void*>)@this->LpVtbl[3])(@this);
    }

    public readonly ulong GetBufferSize()
    {
        SlangBlob* @this = (SlangBlob*)Unsafe.AsPointer(ref Unsafe.AsRef(in this));

        return ((delegate* unmanaged[Stdcall]<SlangBlob*, ulong>)@this->LpVtbl[4])(@this);
    }
};
