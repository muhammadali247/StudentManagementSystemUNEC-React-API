using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.UserExceptions;

public sealed class UserNotFoundByIdException : Exception, IBaseException
{
    public UserNotFoundByIdException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}