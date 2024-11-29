namespace MyFindFeature
{
    internal class FilterLineSource : ILineSource
    {
        private ILineSource parent;
        private Func<Line, bool> f;

        public FilterLineSource(ILineSource parent, Func<Line,bool>? f)
        {
            this.parent = parent;
            this.f = f;
        }
        public void CloseFile()
        {
            parent.CloseFile();
        }

        public void OpenFile()
        {
            parent.OpenFile();
        }

        public Line? ReadLine()
        {
            var line = parent.ReadLine();
            if (line == null) return null;
            else
            {
                 while (line != null && !f(line))
                {
                    line = parent.ReadLine();
                }
                return line;
            }
        }
    }
}