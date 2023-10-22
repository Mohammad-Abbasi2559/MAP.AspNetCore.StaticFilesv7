
namespace MAP.AspNetCore.StaticFiles;

public static class FileExtension
{
    /// <summary>
    /// This method get file extension from file name
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns>return file extension</returns>
    /// <exception cref="ArgumentNullException">Return exception if fileName be null</exception>
    /// <exception cref="ArgumentException">Return exception if fileName doesnt have dot</exception>
    public static string GetFileExtension(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));
        if (!fileName.Contains('.')) throw new ArgumentException("File dont have Extension");

        return fileName[fileName.LastIndexOf('.')..];
    }

    /// <summary>
    /// This method try get file extension from file name
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="extension">Return file extension</param>
    /// <returns>Return get file extension is work or not</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static bool TryGetFileExtension(string fileName, out string? extension)
    {
        if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));
        if (!fileName.Contains('.')) throw new ArgumentException("File dont have Extension");

        bool contentType = FileContentType.TryContentType(fileName);
        
        extension = contentType ? fileName[fileName.LastIndexOf('.')..] : null;

        return contentType;
    }
}
