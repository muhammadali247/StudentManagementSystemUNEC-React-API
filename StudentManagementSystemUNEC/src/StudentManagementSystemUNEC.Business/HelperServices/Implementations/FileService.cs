using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using StudentManagementSystemUNEC.Business.Exceptions.FileExceptions;
using StudentManagementSystemUNEC.Business.Helpers.Extensions;
using StudentManagementSystemUNEC.Business.HelperServices.Interfaces;
using F = System.IO;

namespace StudentManagementSystemUNEC.Business.HelperServices.Implementations;

internal class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> FileUploadAsync(IFormFile file, string path, string type, int size)
    {
        if (!file.CheckFileType(type))
            throw new FileTypeException($"Image must be of {type} type");

        if (!file.CheckFileSize(size))
            throw new FileTypeException($"Image must be of {size} size");

        string fileName = $"{Guid.NewGuid()}-{file.FileName}";
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path, fileName);
        using FileStream fileStream = new FileStream(uploadPath, FileMode.Create);
        file.CopyTo(fileStream);

        return fileName;
    }

    public void DeleteFile(string path)
    {
        if (F.File.Exists(path))
        {
            F.File.Delete(path);
        }
    }
}