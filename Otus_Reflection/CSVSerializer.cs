using System.Text;

namespace Otus_Reflection
{
    class CsvSerializer
    {
        public string? Serialize<T>(T obj)
        {
            var sb = new StringBuilder();
            var type = obj?.GetType();
            var fields = type?.GetFields();

            if (fields is { Length: > 0 })
            {
                // Записываем заголовки столбцов
                foreach (var field in fields)
                {
                    sb.Append(field.Name);
                    sb.Append(",");
                }

                sb.Length--; // Убираем последнюю запятую
                sb.AppendLine();

                // Записываем значения полей
                foreach (var field in fields)
                {
                    sb.Append(field.GetValue(obj));
                    sb.Append(",");
                }

                sb.Length--; // Убираем последнюю запятую
            }

            return sb.ToString();
        }
    }
}