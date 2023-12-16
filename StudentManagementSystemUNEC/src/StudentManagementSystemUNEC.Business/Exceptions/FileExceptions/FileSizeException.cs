using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.FileExceptions;

public sealed class FileSizeException : Exception, IBaseException
{
    public FileSizeException(string message) : base(message)
    {
        ErrorMessage = message;
    }
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}