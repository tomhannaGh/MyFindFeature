namespace MyFindFeature
{
    public interface ILineSource
    {
        void OpenFile();
        void CloseFile();
        Line? ReadLine();
    }
}