using System.Text.Json.Nodes;

namespace Slangc.NET;

public class SlangEntryPoint
{
    internal SlangEntryPoint(JsonObject reader)
    {
        Name = reader["name"].Deserialize<string>();
        Stage = reader["stage"].Deserialize<SlangStage>();
        ThreadGroupSize = reader.ContainsKey("threadGroupSize") ? [.. reader["threadGroupSize"]!.AsArray().Select(static reader => reader!.GetValue<uint>())] : [];
        Bindings = [.. reader["bindings"]!.AsArray().Select(static reader => new SlangNamedTypeBinding(reader!.AsObject()))];
    }

    public string Name { get; }

    public SlangStage Stage { get; }

    public uint[] ThreadGroupSize { get; set; }

    public SlangNamedTypeBinding[] Bindings { get; set; }
}
