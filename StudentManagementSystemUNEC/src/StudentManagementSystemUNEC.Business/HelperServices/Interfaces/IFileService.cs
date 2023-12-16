using Microsoft.AspNetCore.Http;

namespace StudentManagementSystemUNEC.Business.HelperServices.Interfaces;

public interface IFileService
{
    Task<string> FileUploadAsync(IFormFile file, string path, string type, int size);
    void DeleteFile(string path);
}