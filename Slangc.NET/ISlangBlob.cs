using System.Runtime.CompilerServices;

namespace Slangc.NET;

public unsafe struct ISlangBlob
{
    public void** LpVtbl;

    public readonly void* GetBufferPointer()
    {
        ISlangBlob* @this = (ISlangBlob*)Unsafe.AsPointer(ref Unsafe.AsRef(in this));

        return ((delegate* unmanaged[Stdcall]<ISlangBlob*, void*>)@this->LpVtbl[3])(@this);
    }

    public readonly ulong GetBufferSize()
    {
        ISlangBlob* @this = (ISlangBlob*)Unsafe.AsPointer(ref Unsafe.AsRef(in this));

        return ((delegate* unmanaged[Stdcall]<ISlangBlob*, ulong>)@this->LpVtbl[4])(@this);
    }
};
