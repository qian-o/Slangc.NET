namespace Slangc.NET.Models;

public class SlangParameter(string name, SlangBinding binding)
{
    public string Name { get; } = name;

    public SlangBinding Binding { get; } = binding;
}
