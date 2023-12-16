using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.StudentExceptions;


public sealed class StudentAlreadyHasAccountException : Exception, IBaseException
{
    public StudentAlreadyHasAccountException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}