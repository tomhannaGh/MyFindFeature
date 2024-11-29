
using MyFindFeature;

namespace MyFindFeature
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Parameter is empty...");
                return;//ket thuc ngay lap tuc
            }
            var inputOption = BuildFindOption(args);
            var sources = LineSourceFactory.CreateInstance(inputOption.Path,inputOption.SkipFilesOffline);
            foreach (var source in sources)
            {
                FindHandle(source, inputOption);
            }
        }

        private static void FindHandle(ILineSource source, FindOption findOption)
        {
            var count = 0;
            var stringCompase = findOption.IsCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            source = new FilterLineSource(source,
                (line) =>
                    {
                        if (findOption.FindDontContain)
                            return line.TextLine.Contains(findOption.StringToFind, stringCompase) ? false : true;
                        else return line.TextLine.Contains(findOption.StringToFind, stringCompase) ? true : false;
                    });
            try
            {
                source.OpenFile();
                var line = source.ReadLine();
                if (findOption.Count == false)
                {

                    while (line != null)
                    {
                        Print(line);
                        line = source.ReadLine();
                    }
                }
                else
                {
                    while (line != null)
                    {
                        count++;
                        line = source.ReadLine();
                    }
                    PrintCount(count);
                }
            }
            finally { source.CloseFile(); }
        }

        private static void PrintCount(int count)
        {
            Console.Write($": {count}");
        }

        private static void Print(Line line)
        {
            Console.Write($"\n\r[{line.NumberLine}] {line.TextLine}");
        }

        public static FindOption BuildFindOption(string[] args)
        {
            var option = new FindOption();
            foreach (var arg in args)
            {
                if (arg == "/v")
                    option.FindDontContain = true;
                else if (arg == "/c")
                    option.Count = true;
                else if (arg == "/n")
                    option.ShowLineNumber = true;
                else if (arg == "/i")
                    option.IsCaseSensitive = true;
                else if (arg == "/off" || arg == "/offline")
                    option.SkipFilesOffline = false;
                else if (arg == "/?")
                    option.Help = true;
                else
                {
                    if (string.IsNullOrEmpty(option.StringToFind))
                        option.StringToFind = arg;
                    else if (string.IsNullOrEmpty(option.Path))
                        option.Path = arg;
                    else
                        throw new ArgumentException(arg);
                }
            }
            return option;
        }
    }
}