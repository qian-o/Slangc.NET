using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Slangc.NET;

/// <summary>
/// Internal JSON extensions for deserializing reflection data with proper type conversion.
/// </summary>
internal static partial class JsonExtensions
{
    private sealed class NumberToBooleanConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is JsonTokenType.True or JsonTokenType.False)
            {
                return reader.GetBoolean();
            }

            if (reader.TokenType is JsonTokenType.Number)
            {
                if (reader.TryGetUInt64(out ulong value) && value is 0 or 1)
                {
                    return value is 1;
                }

                throw new JsonException("Expected 0 or 1 when parsing boolean.");
            }

            throw new JsonException($"Unexpected token {reader.TokenType} when parsing boolean.");
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteBooleanValue(value);
        }
    }

    [JsonSerializable(typeof(uint))]
    [JsonSerializable(typeof(long))]
    [JsonSerializable(typeof(bool))]
    [JsonSerializable(typeof(double))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(SlangStage))]
    [JsonSerializable(typeof(SlangScopeKind))]
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
            return GetDefault<T>();
        }

        try
        {
            object? value = node.Deserialize(typeof(T), context);

            return value is null ? GetDefault<T>() : (T)value;
        }
        catch (JsonException)
        {
            return GetDefault<T>();
        }
        catch (NotSupportedException)
        {
            return GetDefault<T>();
        }
    }

    private static T GetDefault<T>()
    {
        return typeof(T) == typeof(string) ? (T)(object)string.Empty : default!;
    }

    public static long DeserializeSize(this JsonNode? node)
    {
        if (node is null)
        {
            return 0;
        }

        if (node.GetValueKind() is JsonValueKind.String)
        {
            return node.GetValue<string>() switch
            {
                "unbounded" => -1,
                "unknown" => -2,
                _ => -2
            };
        }

        return (long)node.Deserialize(typeof(long), context)!;
    }
}
