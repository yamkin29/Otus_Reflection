using System.Diagnostics;
using Newtonsoft.Json;

namespace Otus_Reflection;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Сериализуемый класс: class F\n");
        
        // Сериализация в CSV
        var f = F.Get();
        var serializer = new CsvSerializer();

        // Замер времени до сериализации
        var startNew = Stopwatch.StartNew();

        // Сериализация в цикле
        int iterationsCsv = 100000;
        for (int i = 0; i < iterationsCsv; i++)
        {
            var serializedData = serializer.Serialize(f);
            // Вывод сериализованных данных в консоль
            //Console.WriteLine(serializedData);
        }

        startNew.Stop();
        
        Console.WriteLine($"Время выполнения для {iterationsCsv} итераций: {startNew.ElapsedMilliseconds} мс\n");
        
        
        // Сериализация в JSON
        
        // Замер времени до сериализации
        var stopwatch = Stopwatch.StartNew();

        // Сериализация в цикле
        int iterations = 100000;
        for (int i = 0; i < iterations; i++)
        {
            var serializedData = JsonConvert.SerializeObject(f);
            // Вывод сериализованных данных в консоль
            //Console.WriteLine(serializedData);
        }

        stopwatch.Stop();

        // Вывод времени и разницы
        Console.WriteLine($"Время выполнения для {iterations} итераций: {stopwatch.ElapsedMilliseconds} мс\n");
        
        
        
        
        var deserializer = new CsvDeserializer();
        
        Console.WriteLine("код сериализации-десериализации:\n");
        
        Console.WriteLine("// Мой рефлекшен:");
        
        var stopwatchCsv = Stopwatch.StartNew();
        string? csvData = null;
        
        for (int i = 0; i < 100000; i++)
        {
            var serializedData = serializer.Serialize(f);
            csvData = serializedData;
        }
        
        stopwatchCsv.Stop();

        Console.WriteLine($"Время на сериализацию = {stopwatchCsv.ElapsedMilliseconds} мс");
        
        stopwatchCsv.Restart();
        
        for (int i = 0; i < 100000; i++)
        {
            if (csvData != null)
            {
                deserializer.Deserialize<F>(csvData);
            }
        }
        
        stopwatchCsv.Stop();
        
        Console.WriteLine($"Время на десериализацию = {stopwatchCsv.ElapsedMilliseconds} мс\n");

        
        
        
        Console.WriteLine("// Стандартный механизм (NewtonsoftJson):");
        
        var stopwatchJson = Stopwatch.StartNew();
        string? data = null;
        
        for (int i = 0; i < 100000; i++)
        {
            
            var jsonSerialized = JsonConvert.SerializeObject(f);
            data = jsonSerialized;
        }
        
        stopwatchJson.Stop();
        
        Console.WriteLine($"Время на сериализацию = {stopwatchJson.ElapsedMilliseconds} мс");
        
        stopwatchJson.Restart();
        
        for (int i = 0; i < 100000; i++)
        {
            if (data != null)
            {
                JsonConvert.DeserializeObject<F>(data);
            }
        }
        
        stopwatchJson.Stop();
        
        Console.WriteLine($"Время на десериализацию = {stopwatchJson.ElapsedMilliseconds} мс");
    }
}