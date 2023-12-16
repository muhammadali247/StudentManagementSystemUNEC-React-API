using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.AuthExceptions;

public sealed class TokenNotValidException : Exception, IBaseException
{
    public TokenNotValidException(string Message) : base(Message)
    {
        ErrorMessage = Message;

    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}