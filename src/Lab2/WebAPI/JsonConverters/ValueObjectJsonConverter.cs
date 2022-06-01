using System.Text.Json;
using System.Text.Json.Serialization;
using OOP_WebApp.Domain.ValueObjects;

namespace Lab2.WebAPI.JsonConverters;

public class ValueObjectJsonConverter<TValue, TThis> : JsonConverter<TThis>
    where TThis : ValueObject<TValue, TThis>, new()
{
    public override TThis Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        ValueObject<TValue, TThis>.From((TValue) Convert.ChangeType(reader.GetString(), typeof(TValue))!);

    public override void Write(Utf8JsonWriter writer, TThis value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value?.ToString());
    }
}