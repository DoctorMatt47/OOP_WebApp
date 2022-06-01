using System.Text.Json;
using System.Text.Json.Serialization;
using OOP_WebApp.Domain.ValueObjects;

namespace Lab2.WebAPI.JsonConverters;

public class IdJsonConverter<TValue, TId> : JsonConverter<TId> where TId : Id<TValue, TId>, new()
{
    public override TId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        typeof(TValue) == typeof(Guid)
            ? Id<TValue, TId>.From((TValue) Convert.ChangeType(Guid.Parse(reader.GetString()!), typeof(TValue)))
            : Id<TValue, TId>.From((TValue) Convert.ChangeType(reader.GetString(), typeof(TValue))!);

    public override void Write(Utf8JsonWriter writer, TId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value?.ToString());
    }
}