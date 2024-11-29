namespace MyFindFeature
{
    public class LineSourceFactory
    {
        public static ILineSource[] CreateInstance(string path, bool skippOffFile)
        {
            if (path == null)
            {
                return [new ConsoleLineSource()];
            }
            else
            {
                string pattern;
                int idx = path.LastIndexOf(Path.DirectorySeparatorChar);
                if (idx < 0)
                {
                    pattern = path;
                    path = ".";
                }
                else
                {
                    pattern = path[(idx+1)..];
                    path = path[..idx];
                }
                var dir = new DirectoryInfo(path);
                if (dir.Exists)
                {
                    var files = dir.GetFiles(pattern);
                    files.Where(f => !f.Attributes.HasFlag(FileAttributes.Offline)).ToArray();
                    return files.Select(f => new FileLineSource(f.FullName)).ToArray();
                }
            }
            return [];
        }
    }
}