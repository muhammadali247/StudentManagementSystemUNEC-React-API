using Microsoft.AspNetCore.Identity;
using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.AuthExceptions;

public sealed class VerifyUserFailException : Exception, IBaseException
{
    public VerifyUserFailException(IEnumerable<IdentityError> errors)
    {
        ErrorMessage = string.Join(" ", errors.Select(e => e.Description));

    }
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}