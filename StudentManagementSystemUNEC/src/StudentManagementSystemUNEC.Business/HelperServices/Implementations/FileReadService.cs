using StudentManagementSystemUNEC.Business.HelperServices.Interfaces;

namespace StudentManagementSystemUNEC.Business.HelperServices.Implementations;

public class FileReadService : IFileReadService
{
    public string ReadFile(string path, string body)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            body = reader.ReadToEnd();
        }
        return body;
    }
}