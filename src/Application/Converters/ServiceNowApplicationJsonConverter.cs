using System.Text.Json;
using System.Text.Json.Serialization;

public class StringOrObjectConverter<T> : JsonConverter<T> where T : class
{
    /// <summary>
    /// Read converter used to read multiple different json repsonse types to a single type.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="JsonException"></exception>
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            if (string.IsNullOrEmpty(stringValue))
            {
                return null!;
            }

            return (T)(object)stringValue;
        }
        else if (reader.TokenType == JsonTokenType.StartObject)
        {
            return JsonSerializer.Deserialize<T>(ref reader, options)!;
        }

        throw new JsonException($"Unexpected token {reader.TokenType} when reading value of type {typeToConvert}");
    }

    /// <summary>
    /// Write converter used to write to json object.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteStringValue(string.Empty);
        }
        else
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
