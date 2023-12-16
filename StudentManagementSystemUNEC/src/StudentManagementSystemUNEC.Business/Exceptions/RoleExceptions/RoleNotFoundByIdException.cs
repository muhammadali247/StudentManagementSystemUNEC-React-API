using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.RoleExceptions;

public sealed class RoleNotFoundByIdException : Exception, IBaseException
{
    public RoleNotFoundByIdException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}