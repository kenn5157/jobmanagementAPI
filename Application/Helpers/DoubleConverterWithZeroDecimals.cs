using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DoubleConverterWithZeroDecimals : JsonConverter<double>
{
    private const int DecimalPlaces = 6; // Adjust this value to set the desired number of decimal places

    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetDouble();
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("F" + DecimalPlaces.ToString(CultureInfo.InvariantCulture)));
    }
}