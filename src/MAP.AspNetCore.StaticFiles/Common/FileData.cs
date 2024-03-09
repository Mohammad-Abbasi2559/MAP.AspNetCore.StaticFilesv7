namespace MAP.AspNetCore.StaticFiles.Common;

public class FileData
{
    public FileType Type { get; private set; }

    public List<Models.FileExtension> FileExtensions { get; private set; } = new();

    public enum FileType
    {
        AllType = 0,
        Image = 1,
        Video = 2,
        Document = 3,
        Compress = 4,
    }
}