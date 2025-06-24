using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Slangc.NET;

/// <summary>
/// Internal JSON extensions for deserializing reflection data with proper type conversion.
/// </summary>
internal static partial class JsonExtensions
{
    private class NumberToBooleanConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is JsonTokenType.Number)
            {
                return reader.GetUInt32() is not 0;
            }

            throw new JsonException($"Unexpected token {reader.TokenType} when parsing boolean.");
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value ? 1 : 0);
        }
    }

    [JsonSerializable(typeof(uint))]
    [JsonSerializable(typeof(bool))]
    [JsonSerializable(typeof(double))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(SlangStage))]
    [JsonSerializable(typeof(SlangTypeKind))]
    [JsonSerializable(typeof(SlangScalarType))]
    [JsonSerializable(typeof(SlangResourceShape))]
    [JsonSerializable(typeof(SlangResourceAccess))]
    [JsonSerializable(typeof(SlangParameterCategory))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    internal partial class SourceGenerationContext : JsonSerializerContext;

    private static readonly SourceGenerationContext context = new(new()
    {
        Converters = { new NumberToBooleanConverter() }
    });

    public static T Deserialize<T>(this JsonNode? node)
    {
        if (node is null)
        {
            return default!;
        }

        return (T)node.Deserialize(typeof(T), context)!;
    }
}
