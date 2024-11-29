namespace MyFindFeature
{
    internal class FileLineSource: ILineSource
    {
        private readonly string fullName;
        private int numberLine;
        private StreamReader sr;

        public FileLineSource(string fullName)
        {
            this.fullName = fullName;
        }

        public void CloseFile()
        {
            if (sr != null)
            {
                sr.Close();
                sr = null;
            }
        }

        public void OpenFile()
        {
            if(sr != null) 
                throw new InvalidOperationException();
            Console.Write($"-------------------{fullName.Split(Path.DirectorySeparatorChar).Last()}");
            numberLine = 0;
            sr = new StreamReader(new FileStream(fullName, FileMode.Open, FileAccess.Read));
        }

        public Line? ReadLine()
        {
            if (sr == null)
                throw new InvalidOperationException();
            var text = sr.ReadLine();
            if (text == null)
                return null;
            else
                return new Line() { NumberLine = ++numberLine , TextLine = text}; 
        }
    }
}