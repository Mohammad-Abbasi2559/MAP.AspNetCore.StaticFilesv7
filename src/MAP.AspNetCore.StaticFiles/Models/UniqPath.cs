namespace MAP.AspNetCore.StaticFiles.Models;

public class UniqPath
{
    public string Name { get; set; } = string.Empty;
    
    public string FileName { get; set; } = string.Empty;
    
    public string FullPath { get; set; } = string.Empty;

    public string RootPath { get; set; } = string.Empty;

    public string ResultPath { get; set; } = string.Empty;

    public bool IsSuccess { get; set; } = true;

    public string Error { get; set; } = string.Empty;
}