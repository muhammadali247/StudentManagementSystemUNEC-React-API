using Microsoft.AspNetCore.Identity;
using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.UserExceptions;

public sealed class UpdateUserFailException : Exception, IBaseException
{
    public UpdateUserFailException(IEnumerable<IdentityError> errors)
    {
        ErrorMessage = string.Join(" ", errors.Select(e => e.Description));

    }
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}