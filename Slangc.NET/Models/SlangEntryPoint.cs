using System.Text.Json.Nodes;
using Slangc.NET.Enums;

namespace Slangc.NET.Models;

public class SlangEntryPoint
{
    internal SlangEntryPoint(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Stage = reader["stage"].Deserialize<SlangStage>();
        Bindings = [.. reader["bindings"]!.AsArray().Select(static item => new SlangParameterBinding(item!.AsObject()))];
    }

    public string Name { get; }

    public SlangStage Stage { get; }

    public SlangParameterBinding[] Bindings { get; set; }
}
