using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DoubleConverterWithZeroDecimals : JsonConverter<double>
{
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetDouble();
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        string formattedValue = value.ToString("0.0#############", CultureInfo.InvariantCulture);
        writer.WriteStringValue(formattedValue);
    }
}