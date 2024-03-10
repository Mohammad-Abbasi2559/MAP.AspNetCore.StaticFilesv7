using MAP.AspNetCore.StaticFiles.Models;

namespace MAP.AspNetCore.StaticFiles.Common;

public static class SaveUniq
{
    internal static string WebHost() => Directory.GetCurrentDirectory();

    public static UniqPath GetUniqName(string fileName, string path)
    {
        string ext = FileOperation.GetExtensionWithDot(fileName);
        path = !path.EndsWith("/") ? path + "/" : path;
        if (File.Exists(path + fileName))
        {
            int index = 2;
            string name = Path.GetFileNameWithoutExtension(fileName);
            name = name + $"({index})" + ext;
            while (File.Exists(path + name))
            {
                name = name.Replace($"({index}){ext}", $"({index + 1}){ext}");
                index ++;
            }
            var resultPath = path.Contains("wwwroot") ? path.Split("wwwroot")[1] + name : path.Replace(WebHost(), string.Empty) + name;
            return new() { FileName = name, RootPath = path, FullPath = path + name, ResultPath = resultPath };
        }
        else
        {
            var resultPath = path.Contains("wwwroot") ? path.Split("wwwroot")[1] + fileName : path.Replace(WebHost(), string.Empty) + fileName;
            return new() { FileName = fileName, RootPath = path, FullPath = path + fileName, ResultPath = resultPath };
        }
    }
}