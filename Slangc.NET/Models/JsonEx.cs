using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Slangc.NET.Models;

internal static partial class JsonEx
{
    [JsonSerializable(typeof(string))]
    internal partial class SourceGenerationContext : JsonSerializerContext;

    private static readonly SourceGenerationContext context = new();

    public static void Foreach(this JsonNode? node, Action<JsonNode?> action)
    {
        if (node is null)
        {
            return;
        }

        foreach (JsonNode? item in node.AsArray())
        {
            action(item);
        }
    }

    public static T Deserialize<T>(this JsonNode? node)
    {
        if (node is null)
        {
            return default!;
        }

        return (T)node.Deserialize(typeof(T), context)!;
    }
}
