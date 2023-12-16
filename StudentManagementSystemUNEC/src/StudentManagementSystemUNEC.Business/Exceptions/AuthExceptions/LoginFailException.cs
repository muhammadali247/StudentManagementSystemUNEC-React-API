using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.AuthExceptions;

public sealed class LoginFailException : Exception, IBaseException
{
    public LoginFailException(string Message) : base(Message)
    {
        ErrorMessage = Message;

    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage  { get; }
}