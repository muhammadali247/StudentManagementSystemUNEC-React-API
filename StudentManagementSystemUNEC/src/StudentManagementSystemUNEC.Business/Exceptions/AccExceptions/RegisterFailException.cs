using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.AccExceptions;

 public sealed class RegisterFailException : Exception, IBaseException
{
    public RegisterFailException(string Message) : base(Message)
    {
        ErrorMessage = Message;

    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage  { get; }
}