using MAP.AspNetCore.StaticFiles.Models;

namespace MAP.AspNetCore.StaticFiles.Common;

public class FileData
{
    private FileData(FileType type)
    {
        Type = type;
        FileExtensions = type switch
        {
            FileType.Image => GetImageData(),
            FileType.Video => GetVideoData(),
            FileType.Document => GetDocumentData(),
            FileType.Compress => GetCompressData(),
            _ => GetExtensionsData(),
        };
    }

    public static FileData GetFileData(FileType type) => new(type);

    public static Models.FileExtension? GetFileExtensionData(string extension) => GetExtensionsData().FirstOrDefault(i => i.Extension == extension);

    public static Models.FileExtension? GetImageExtensionData(string extension) => GetImageData().FirstOrDefault(i => i.Extension == extension);

    public static Models.FileExtension? GetVideoExtensionData(string extension) => GetVideoData().FirstOrDefault(i => i.Extension == extension);

    public static Models.FileExtension? GetDocumentExtensionData(string extension) => GetDocumentData().FirstOrDefault(i => i.Extension == extension);

    public static Models.FileExtension? GetCompressExtensionData(string extension) => GetCompressData().FirstOrDefault(i => i.Extension == extension);

    public static string GetMimeType(string extension) => GetExtensionsData().Where(i => i.Extension == extension).Select(i => i.MimeType).FirstOrDefault() ?? "application/octet-stream";

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


    public static List<Models.FileExtension> GetExtensionsData()
    {
        return new List<Models.FileExtension>()
        {
            //* IMAGE
            //? jpg
            new()
            {
                Extension = "jpg",
                MimeType = "image/jpeg",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 255, 216, 255 } } }
            },

            //?jpeg
            new()
            {
                Extension = "jpeg",
                MimeType = "image/jpeg",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 255, 216, 255 } } },
                LastChunkBytes = new List<FileHeader>() { new() { Offset = 2, ChunkBytes = new byte?[] { 255, 217 } } }
            },

            //?bmp
            new()
            {
                Extension = "bmp",
                MimeType = "image/bmp",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 66, 77 } } }
            },

            //?gif
            new()
            {
                Extension = "gif",
                MimeType = "image/gif",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 71, 73, 70, 56 } } }
            },

            //?png
            new()
            {
                Extension = "png",
                MimeType = "image/png",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 137, 80, 78, 71, 13, 10, 26, 10 } } }
            },

            //?webp
            new()
            {
                Extension = "webp",
                MimeType = "image/webp",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 82, 73, 70, 70} }, new() { Offset = 0, ChunkBytes = new byte?[] { 87, 69, 66, 80} } }
            },

            //?ico
            new()
            {
                Extension = "ico",
                MimeType = "image/vnd.microsoft.icon",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 0, 0, 1, 0 } } }
            },

            //?icns
            new()
            {
                Extension = "icns",
                MimeType = "image/icns",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 105, 99, 110, 115 } } }
            },

            //*VIDEO

            //?wmv
            new()
            {
                Extension = "wmv",
                MimeType = "video/x-ms-asf",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 48, 38, 178, 117, 142, 102, 207, 17 } }, new() { Offset = 0, ChunkBytes = new byte?[] { 166, 217, 0, 170, 0, 98, 206, 108 } } }
            },

            //?avi
            new()
            {
                Extension = "avi",
                MimeType = "avi/x-msvideo",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 82, 73, 70, 70, 65, 86, 73, 32 } } }
            },

            //?flv
            new()
            {
                Extension = "flv",
                MimeType = "video/x-flv",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 70, 76, 86 } } }
            },

            //?mpg
            new()
            {
                Extension = "mpg",
                MimeType = "video/mpeg",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] {0, 0, 1, 186 } } }
            },

            //?mpeg
            new()
            {
                Extension = "mpeg",
                MimeType = "video/mpeg",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] {0, 0, 1, 186 } } }
            },

            //?mp4
            new()
            {
                Extension = "mp4",
                MimeType = "video/mp4",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 4, ChunkBytes = new byte?[] { 102, 116, 121, 112, 109, 109, 112, 52 } } }
            },

            //?webm
            new()
            {
                Extension = "webm",
                MimeType = "video/webm",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 26, 69, 223, 163 } } }
            },

            //?mkv
            new()
            {
                Extension = "mkv",
                MimeType = "video/x-mastroska",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 26, 69, 223, 163 } } }
            },

            //?m4v
            new()
            {
                Extension = "m4v",
                MimeType = "video/x-m4v",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 4, ChunkBytes = new byte?[] { 102, 116, 121, 112, 77, 52, 86, 32 } } }
            },

            //*DOCUMENT

            //?pdf
            new()
            {
                Extension = "pdf",
                MimeType = "application/pdf",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 37, 80, 68, 70, 45 } } }
            },

            //?doc
            new()
            {
                Extension = "doc",
                MimeType = "application/msword",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 208, 207, 17, 224 } } }
            },

            //?docx
            new()
            {
                Extension = "docx",
                MimeType = "application/vnd.openxmlformats",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 80, 75, 3, 4 } } }
            },

            //?ppt
            new()
            {
                Extension = "ppt",
                MimeType = "application/vnd.ms-powerpoint",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 208, 207, 17, 224, 161, 171, 26, 225 } } }
            },

            //?pptx
            new()
            {
                Extension = "pptx",
                MimeType = "application/vndopenxmlformats",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 80, 75, 3, 4 } } }
            },

            //?xlsx
            new()
            {
                Extension = "xlsx",
                MimeType = "application/vnd.openxmlformats",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 80, 75, 3, 4 } } }
            },

            //?xls
            new()
            {
                Extension = "xls",
                MimeType = "application/vnd.ms-excel",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 208, 207, 17, 224, 161, 177, 26, 225 } } }
            },

            //?html
            new()
            {
                Extension = "html",
                MimeType = "text/html",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 60, 104, 116, 109, 108 } } },
                LastChunkBytes = new List<FileHeader>() { new() { Offset = 6, ChunkBytes = new byte?[] { 60, 104, 116, 109, 108, 62 } } }
            },

            //*COMPRESS

            //?zip
            new()
            {
                Extension = "zip",
                MimeType = "application/zip",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 80, 75, 3, 4 } } }
            },

            //?rar
            new()
            {
                Extension = "rar",
                MimeType = "application/x-rar-compressed",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 82, 97, 114, 33 } } }
            }
        };
    }

    public static List<Models.FileExtension> GetImageData()
    {
        return new List<Models.FileExtension>()
        {
            //? jpg
            new()
            {
                Extension = "jpg",
                MimeType = "image/jpeg",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 255, 216, 255 } } }
            },

            //?jpeg
            new()
            {
                Extension = "jpeg",
                MimeType = "image/jpeg",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 255, 216, 255 } } },
                LastChunkBytes = new List<FileHeader>() { new() { Offset = 2, ChunkBytes = new byte?[] { 255, 217 } } }
            },

            //?bmp
            new()
            {
                Extension = "bmp",
                MimeType = "image/bmp",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 66, 77 } } }
            },

            //?gif
            new()
            {
                Extension = "gif",
                MimeType = "image/gif",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 71, 73, 70, 56 } } }
            },

            //?png
            new()
            {
                Extension = "png",
                MimeType = "image/png",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 137, 80, 78, 71, 13, 10, 26, 10 } } }
            },

            //?webp
            new()
            {
                Extension = "webp",
                MimeType = "image/webp",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 82, 73, 70, 70} }, new() { Offset = 0, ChunkBytes = new byte?[] { 87, 69, 66, 80} } }
            },

            //?ico
            new()
            {
                Extension = "ico",
                MimeType = "image/vnd.microsoft.icon",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 0, 0, 1, 0 } } }
            },

            //?icns
            new()
            {
                Extension = "icns",
                MimeType = "image/icns",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 105, 99, 110, 115 } } }
            },
        };
    }

    public static List<Models.FileExtension> GetVideoData()
    {
        return new List<Models.FileExtension>()
        {
            //?wmv
            new()
            {
                Extension = "wmv",
                MimeType = "video/x-ms-asf",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 48, 38, 178, 117, 142, 102, 207, 17 } }, new() { Offset = 0, ChunkBytes = new byte?[] { 166, 217, 0, 170, 0, 98, 206, 108 } } }
            },

            //?avi
            new()
            {
                Extension = "avi",
                MimeType = "avi/x-msvideo",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 82, 73, 70, 70, 65, 86, 73, 32 } } }
            },

            //?flv
            new()
            {
                Extension = "flv",
                MimeType = "video/x-flv",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 70, 76, 86 } } }
            },

            //?mpg
            new()
            {
                Extension = "mpg",
                MimeType = "video/mpeg",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] {0, 0, 1, 186 } } }
            },

            //?mpeg
            new()
            {
                Extension = "mpeg",
                MimeType = "video/mpeg",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] {0, 0, 1, 186 } } }
            },

            //?mp4
            new()
            {
                Extension = "mp4",
                MimeType = "video/mp4",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 4, ChunkBytes = new byte?[] { 102, 116, 121, 112, 109, 109, 112, 52 } } }
            },

            //?webm
            new()
            {
                Extension = "webm",
                MimeType = "video/webm",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 26, 69, 223, 163 } } }
            },

            //?mkv
            new()
            {
                Extension = "mkv",
                MimeType = "video/x-mastroska",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 26, 69, 223, 163 } } }
            },

            //?m4v
            new()
            {
                Extension = "m4v",
                MimeType = "video/x-m4v",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 4, ChunkBytes = new byte?[] { 102, 116, 121, 112, 77, 52, 86, 32 } } }
            }
        };
    }

    public static List<Models.FileExtension> GetDocumentData()
    {
        return new List<Models.FileExtension>()
        {
            //?pdf
            new()
            {
                Extension = "pdf",
                MimeType = "application/pdf",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 37, 80, 68, 70, 45 } } }
            },

            //?doc
            new()
            {
                Extension = "doc",
                MimeType = "application/msword",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 208, 207, 17, 224 } } }
            },

            //?svg
            new()
            {
                Extension = "svg",
                MimeType = "image/svg+xml",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 60, 115, 118, 103 } } },
                LastChunkBytes = new List<FileHeader>() { new() { Offset = 6, ChunkBytes = new byte?[] { 60, 47, 115, 118, 103, 62} } }
            },

            //?docx
            new()
            {
                Extension = "docx",
                MimeType = "application/vnd.openxmlformats",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 80, 75, 3, 4 } } }
            },

            //?ppt
            new()
            {
                Extension = "ppt",
                MimeType = "application/vnd.ms-powerpoint",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 208, 207, 17, 224, 161, 171, 26, 225 } } }
            },

            //?pptx
            new()
            {
                Extension = "pptx",
                MimeType = "application/vndopenxmlformats",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 80, 75, 3, 4 } } }
            },

            //?xlsx
            new()
            {
                Extension = "xlsx",
                MimeType = "application/vnd.openxmlformats",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 80, 75, 3, 4 } } }
            },

            //?xls
            new()
            {
                Extension = "xls",
                MimeType = "application/vnd.ms-excel",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 208, 207, 17, 224, 161, 177, 26, 225 } } }
            },

            //?html
            new()
            {
                Extension = "html",
                MimeType = "text/html",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 60, 104, 116, 109, 108 } } },
                LastChunkBytes = new List<FileHeader>() { new() { Offset = 6, ChunkBytes = new byte?[] { 60, 104, 116, 109, 108, 62 } } }
            }
        };
    }

    public static List<Models.FileExtension> GetCompressData()
    {
        return new List<Models.FileExtension>()
        {
            //?zip
            new()
            {
                Extension = "zip",
                MimeType = "application/zip",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 80, 75, 3, 4 } } }
            },

            //?rar
            new()
            {
                Extension = "rar",
                MimeType = "application/x-rar-compressed",
                FirstChunkBytes = new List<FileHeader>() { new() { Offset = 0, ChunkBytes = new byte?[] { 82, 97, 114, 33 } } }
            }
        };
    }
}