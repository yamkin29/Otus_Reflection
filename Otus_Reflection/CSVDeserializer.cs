namespace Otus_Reflection
{
    class CsvDeserializer
    {
        public T Deserialize<T>(string? csvData) where T : new()
        {
            var obj = new T();
            var lines = csvData?.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            
            if (lines != null && lines.Length == 2)
            {
                var fieldNames = lines[0].Split(',');
                var fieldValues = lines[1].Split(',');

                var fields = typeof(T).GetFields();
                for (int i = 0; i < Math.Min(fieldNames.Length, fieldValues.Length); i++)
                {
                    var fieldName = fieldNames[i].Trim();
                    var fieldValue = fieldValues[i].Trim();

                    var field = fields.FirstOrDefault(f => f.Name == fieldName);
                    if (field != null)
                    {
                        var value = Convert.ChangeType(fieldValue, field.FieldType);
                        field.SetValue(obj, value);
                    }
                }
            }

            return obj;
        }
    }
}