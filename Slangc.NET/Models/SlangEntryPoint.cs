using System.Text.Json.Nodes;
using Slangc.NET.Enums;

namespace Slangc.NET.Models;

public class SlangEntryPoint
{
    internal SlangEntryPoint(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Stage = reader["stage"].Deserialize<SlangStage>();
        Bindings = [.. reader["bindings"]!.AsArray().Select(static reader => new SlangNamedTypeBinding(reader!.AsObject()))];

        if (Stage is SlangStage.Compute)
        {
            ThreadGroupSize = [.. reader["threadGroupSize"]!.AsArray().Select(static reader => reader!.GetValue<uint>())];
        }
        else
        {
            ThreadGroupSize = [];
        }
    }

    public string Name { get; }

    public SlangStage Stage { get; }

    public SlangNamedTypeBinding[] Bindings { get; set; }

    public uint[] ThreadGroupSize { get; set; }
}
