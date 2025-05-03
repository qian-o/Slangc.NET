using Slangc.NET.Enums;

namespace Slangc.NET.Models;

public class SlangBinding(SlangParameterCategory kind, uint space, uint index, bool used)
{
    public SlangParameterCategory Kind { get; } = kind;

    public uint Space { get; } = space;

    public uint Index { get; } = index;

    public bool Used { get; } = used;
}
