using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.AccExceptions;

public sealed class UserNotRegisteredException : Exception, IBaseException
{
    public UserNotRegisteredException(string Message) : base(Message)
    {
        ErrorMessage = Message;

    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}