namespace Otus_Reflection;

class CsvDeserializer
{
    public T Deserialize<T>(string? csvData) where T : new()
    {
        var obj = new T();
        var lines = csvData.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        var properties = typeof(T).GetProperties();

        foreach (var line in lines)
        {
            var values = line.Split(',');

            for (int i = 0; i < Math.Min(properties.Length, values.Length); i++)
            {
                var property = properties[i];
                var value = Convert.ChangeType(values[i], property.PropertyType);
                property.SetValue(obj, value);
            }
        }

        return obj;
    }
}