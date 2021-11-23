using Newtonsoft.Json;

namespace RetSim.Data.JSON;

public static class JSONExtensions
{
    public static void WriteData(this JsonWriter writer, string property, object value)
    {
        writer.WritePropertyName(property);
        writer.WriteValue(value);
    }

    public static void WriteArray(this JsonWriter writer, string property, object[] array, string label)
    {
        writer.WritePropertyName(property);
        writer.WriteStartArray();

        for (int i = 0; i < array.Length; i++)
        {
            writer.WriteStartObject();

            writer.WritePropertyName(label);
            writer.WriteValue(array[i]);

            writer.WriteEndObject();
        }

        writer.WriteEndArray();
    }

    public static void WriteIntArray(this JsonWriter writer, string property, int[] array)
    {
        writer.WritePropertyName(property);
        writer.WriteStartArray();

        for (int i = 0; i < array.Length; i++)
        {
            writer.WriteValue(array[i]);
        }

        writer.WriteEndArray();
    }

    public static void WriteStringArray(this JsonWriter writer, string property, string[] array)
    {
        writer.WritePropertyName(property);
        writer.WriteStartArray();

        for (int i = 0; i < array.Length; i++)
        {
            writer.WriteValue(array[i]);
        }

        writer.WriteEndArray();
    }

    public static void WriteDictionary(this JsonWriter writer, string property, object[] array1, object[] array2, string label1, string label2)
    {
        writer.WritePropertyName(property);
        writer.WriteStartArray();

        for (int i = 0; i < array1.Length; i++)
        {
            writer.WriteStartObject();

            writer.WritePropertyName(label1);
            writer.WriteValue(array1[i]);

            writer.WritePropertyName(label2);
            writer.WriteValue(array2[i]);

            writer.WriteEndObject();
        }

        writer.WriteEndArray();
    }
}