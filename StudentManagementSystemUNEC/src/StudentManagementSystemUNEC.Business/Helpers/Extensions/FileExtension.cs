using Microsoft.AspNetCore.Http;

namespace StudentManagementSystemUNEC.Business.Helpers.Extensions;

public static class FileExtension
{
    public static bool CheckFileType(this IFormFile file, string type) => file.ContentType.Contains(type);
    public static bool CheckFileSize(this IFormFile file, int size) => file.Length / 1024 < size;
}