using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Slangc.NET.Enums;

namespace Slangc.NET.Models;

internal static partial class JsonEx
{
    [JsonSerializable(typeof(uint))]
    [JsonSerializable(typeof(bool))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(SlangParameterCategory))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    internal partial class SourceGenerationContext : JsonSerializerContext;

    private static readonly SourceGenerationContext context = new();

    public static void Foreach(this JsonNode? node, Action<JsonObject> action)
    {
        if (node is null || node is not JsonArray array)
        {
            return;
        }

        foreach (JsonNode? item in array)
        {
            action(item!.AsObject());
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
