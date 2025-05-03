using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Slangc.NET.Enums;

namespace Slangc.NET;

internal static partial class JsonExtensions
{
    [JsonSerializable(typeof(uint))]
    [JsonSerializable(typeof(bool))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(SlangTypeKind))]
    [JsonSerializable(typeof(SlangScalarType))]
    [JsonSerializable(typeof(SlangResourceShape))]
    [JsonSerializable(typeof(SlangResourceAccess))]
    [JsonSerializable(typeof(SlangParameterCategory))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    internal partial class SourceGenerationContext : JsonSerializerContext;

    private static readonly SourceGenerationContext Context = new();

    public static T Deserialize<T>(this JsonNode? node)
    {
        if (node is null)
        {
            return default!;
        }

        return (T)node.Deserialize(typeof(T), Context)!;
    }
}
