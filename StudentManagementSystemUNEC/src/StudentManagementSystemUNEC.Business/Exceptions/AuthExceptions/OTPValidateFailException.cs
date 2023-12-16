using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.AuthExceptions;


public sealed class OTPValidateFailException : Exception, IBaseException
{
    public OTPValidateFailException(string Message) : base(Message)
    {
        ErrorMessage = Message;

    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}