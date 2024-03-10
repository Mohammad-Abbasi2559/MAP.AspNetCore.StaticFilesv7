using Microsoft.AspNetCore.Http;

namespace MAP.AspNetCore.StaticFiles.Common;

/// <summary>
/// Common needed operation that working in the project
/// </summary>
public class FileOperation
{
    internal static async Task<byte[]> GetBytesAsync(string path) => await File.ReadAllBytesAsync(path);

    internal static string GetExtensionWithOutDot(string fileNameOrPath)
    {
        string ext = Path.GetExtension(fileNameOrPath).ToLowerInvariant();

        return ext.StartsWith(".") ? ext.Remove(0, 1) : ext;
    }

    internal static string GetExtensionWithDot(string fileNameOrPath)
    {
        return Path.GetExtension(fileNameOrPath).ToLowerInvariant();
    }

    internal static async Task<byte[]> GetBytesAsync(IFormFile file)
    {
        using MemoryStream stream = new();
        await file.CopyToAsync(stream);
        byte[] bytes = stream.ToArray();
        stream.Close();
        return bytes;
    }

    internal static byte[] GetBytes(IFormFile file)
    {
        using MemoryStream stream = new();
        file.CopyTo(stream);
        byte[] bytes = stream.ToArray();
        stream.Close();
        return bytes;
    }
}