using System.ComponentModel.DataAnnotations;

namespace MAP.AspNetCore.StaticFiles.Models;

public class FileHeader
{
    public int Offset { get; set; }

    [Required]
    public byte?[]? ChunkBytes { get; set; }
}