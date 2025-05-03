using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Slangc.NET.Models;

internal static partial class JsonEx
{
    [JsonSerializable(typeof(string))]
    internal partial class SourceGenerationContext : JsonSerializerContext;

    private static readonly SourceGenerationContext context = new();

    public static void Foreach(this JsonNode node, Action<JsonObject> action)
    {
        foreach (JsonNode? item in node.AsArray())
        {
            action(item!.AsObject());
        }
    }

    public static T Deserialize<T>(this JsonNode node)
    {
        return (T)node.Deserialize(typeof(T), context)!;
    }
}
