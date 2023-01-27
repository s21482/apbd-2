using System.Text.Json;


namespace DataProcessor
{
    public class Program
    {
        private static string _outputDir;
        private static List<string> _errorLog = new List<string>();
        public static void Main(string[] args)
        {
            if (args.Length != 3)
                throw new ArgumentException("Podaj dokładnie 3 argumenty");

            _outputDir = args[0];
            string csvDir = args[1];
            string outputFormat = args[2];

            if(!Directory.Exists(_outputDir))
            {
                throw new ArgumentException("Podana ścieżka jest nieprawidłowa");
            }

            if(!File.Exists(csvDir))
            {
                throw new FileNotFoundException($"Plik {csvDir} nie istnieje");
            }

            if(outputFormat != "json")
            {
                throw new ArgumentException($"Dostępna jest jedynie konwersja do formatu json, podałeś {outputFormat}");
            }

            StreamReader streamReader = new StreamReader(csvDir);
            HashSet<Student> students = new HashSet<Student>(new Student());

            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                string[] values = line.Split(',');
                if (values.Length != 9)
                {
                    _errorLog.Add(line);
                    continue;
                }
                if (values.Any(value => string.IsNullOrWhiteSpace(value)))
                {
                    _errorLog.Add(line);
                    continue;
                }
                {
                Student student = new Student
                {
                    Fname = values[0],
                    Lname = values[1],
                    IndexNumber = values[4],
                    Birthdate = values[5],
                    Email = values[6],
                    MothersName = values[7],
                    FathersName = values[8],
                    Studies = new Studies
                    {
                        Name = values[2],
                        Mode = values[3]
                    }
                };
                students.Add(student);
                }
            }

            StreamWriter streamWriter = new StreamWriter($"{_outputDir}/result.json");

            streamWriter.Write(JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));

            CreateErrorLog();

            streamReader.Close();
            streamWriter.Close();

        }
        private static void CreateErrorLog()
        {
            using StreamWriter streamWriter = new StreamWriter($"{_outputDir}/log.txt");
            foreach (string error in _errorLog)
            {
                streamWriter.WriteLine(error);
            }
            streamWriter.Close();
        }
    }
}