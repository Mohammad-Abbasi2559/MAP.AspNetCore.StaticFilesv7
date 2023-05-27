
namespace MAP.AspNetCore.StaticFiles;

public static class FileExtension
{
    /// <summary>
    /// This method get file extension from file name
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns>return file extension</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static string? GetFileExtension(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("fileName is Rquired");
        if (!fileName.Contains(".")) throw new ArgumentException("File dont have Extension");

        return fileName.Split('.').LastOrDefault();
    }
}
