using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.AuthExceptions;


public sealed class VerificationValidationFailException : Exception, IBaseException
{
    public VerificationValidationFailException(string Message) : base(Message)
    {
        ErrorMessage = Message;

    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}