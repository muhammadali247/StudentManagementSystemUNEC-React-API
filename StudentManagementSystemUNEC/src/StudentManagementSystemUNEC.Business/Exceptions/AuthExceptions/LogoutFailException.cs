using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.AuthExceptions;

public sealed class LogoutFailException : Exception, IBaseException
{
    public LogoutFailException(string Message) : base(Message)
    {
        ErrorMessage = Message;

    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}