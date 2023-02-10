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
                linesFound.ForEach(x => Console.WriteLine($"{TAB}=> {x.lineNumber.ToString().PadLeft(padSize, ' ')} | {x.line}"));
            }

            Console.WriteLine($"\n{separator}");
        }
    }
}