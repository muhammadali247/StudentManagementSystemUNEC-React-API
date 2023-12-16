using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.UserExceptions;

public sealed class UserAlreadyHasTeacherException : Exception, IBaseException
{
    public UserAlreadyHasTeacherException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}