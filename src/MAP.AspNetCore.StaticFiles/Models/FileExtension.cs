using System.ComponentModel.DataAnnotations;

namespace MAP.AspNetCore.StaticFiles.Models;

public class FileExtension
{
    public string Extension { get; set; } = string.Empty;

    public string MimeType { get; set; } = string.Empty;




    [Required]
    public List<FileHeader>? FirstChunkBytes { get; set; }

    public List<FileHeader>? LastChunkBytes { get; set; }
}