using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.TeacherExceptions;

public sealed class TeacherAlreadyHasAccountException : Exception, IBaseException
{
    public TeacherAlreadyHasAccountException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}