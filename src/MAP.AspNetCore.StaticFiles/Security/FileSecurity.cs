using MAP.AspNetCore.StaticFiles.Common;

namespace MAP.AspNetCore.StaticFiles.Security;

public static class FileSecurity
{
    private static bool FirstChunkBytesValid(Models.FileExtension extension, byte[] bytes)
    {
        var firstChunkBytes = extension.FirstChunkBytes!.AsQueryable().GroupBy(o => o.Offset).Select(i => new { Offset = i.Key, Headers = i });

        List<bool> firstChunkBytesCheck = new();
        foreach (var item in firstChunkBytes)
        {
            bool check = false;
            foreach (var header in item.Headers)
            {
                int j = 0;
                int count = header.ChunkBytes!.Count();
                for (int i = header.Offset; i < count; i++)
                {
                    if (bytes[i] == header.ChunkBytes![i])
                    {
                        j++;
                        if (j == count) check = true;
                    }
                }
            }
            firstChunkBytesCheck.Add(check);
        }

        return firstChunkBytesCheck.All(i => i);
    }

    private static bool FinalChunkBytesValid(Models.FileExtension extension, byte[] bytes)
    {
        if (extension.LastChunkBytes == null) return true;
        var finalChunkBytes = extension.LastChunkBytes.AsQueryable().GroupBy(o => o.Offset).Select(i => new { Offset = i.Key, Headers = i });

        List<bool> finalChunkBytesCheck = new();
        foreach (var item in finalChunkBytes)
        {
            bool check = false;
            foreach (var header in item.Headers)
            {
                int j = 0;
                int offset = 0;
                long count = bytes.LongCount();
                long index = count - header.Offset;
                for (long i = index; i < count; i++)
                {
                    if (bytes[i] == header.ChunkBytes![offset])
                    {
                        j++;
                        if (j == header.Offset) check = true;
                    }
                    offset++;
                }
            }
            finalChunkBytesCheck.Add(check);
        }

        return finalChunkBytesCheck.All(i => i);
    }

    public static async Task<bool> IsValidFile(string path)
    {
        string ext = FileOperation.GetExtensionWithOutDot(path);

        Models.FileExtension? extension = FileData.GetFileExtensionData(ext);

        byte[] bytes = await File.ReadAllBytesAsync(path);
        
        return extension != null && FinalChunkBytesValid(extension, bytes) && FirstChunkBytesValid(extension, bytes);
    }
}