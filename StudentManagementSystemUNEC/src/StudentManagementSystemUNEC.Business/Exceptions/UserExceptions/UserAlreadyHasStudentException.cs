using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.UserExceptions;

public sealed class UserAlreadyHasStudentException : Exception, IBaseException
{
    public UserAlreadyHasStudentException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}