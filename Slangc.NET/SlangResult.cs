namespace Slangc.NET;

public readonly struct SlangResult(int code)
{
    public bool IsOk => code is 0;

    public bool IsError => code is not 0;

    public static implicit operator SlangResult(int code)
    {
        return new(code);
    }
}
