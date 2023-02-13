using System.Text.RegularExpressions;

namespace FindRegexTextFile
{
    class Program
    {
        private struct Line
        {
            public int lineNumber;
            public string line;

            public Line(int lineNumber, string line)
            {
                this.lineNumber = lineNumber;
                this.line = line;
            }
        }

        static public void Main(String[] args)
        {
            const string TAB = " ";
            var separator = new String('*', 100);

            var fileName = args[0];
            var regex = new Regex(args[1]);

            string outputFile = args.Count() > 2 && Directory.Exists(Path.GetDirectoryName(args[2])) ? args[2] : Path.GetTempFileName();

            var linesFound = new List<Line>();

            using (var sr = new StreamReader(fileName))
            {
                var line = string.Empty;
                int lineNumber = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    lineNumber++;

                    if (regex.IsMatch(line)) linesFound.Add(new Line(lineNumber, line));
                }
            }

            Console.WriteLine(separator);

            if (linesFound.Count == 0) { Console.WriteLine("\nNo lines found with regex."); }
            else
            {
                Console.WriteLine($"\nLines found:");

                var padSize = linesFound.LastOrDefault().lineNumber.ToString().Length + 1;

                using (var sw = new StreamWriter(outputFile) { AutoFlush = true })
                {
                    foreach (var line in linesFound)
                    {
                        var print = $"{TAB}=> {line.lineNumber.ToString().PadLeft(padSize, ' ')} | {line.line}";
                        Console.WriteLine(print);

                        if (string.IsNullOrEmpty(outputFile)) continue;


                        sw.WriteLine(print);
                        sw.Flush();
                    }
                }
            }

            Console.WriteLine($"\n{separator}\n");
            Console.WriteLine($"Saved in \"{outputFile}\"");
        }
    }
}