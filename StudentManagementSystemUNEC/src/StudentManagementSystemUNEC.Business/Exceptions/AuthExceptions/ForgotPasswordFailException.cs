using Microsoft.AspNetCore.Identity;
using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.AuthExceptions;

public sealed class ForgotPasswordFailException : Exception, IBaseException
{
    public ForgotPasswordFailException(string Message) : base(Message)
    {
        ErrorMessage = Message;

    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}