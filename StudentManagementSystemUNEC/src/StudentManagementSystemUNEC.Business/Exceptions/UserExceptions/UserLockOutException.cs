using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.UserExceptions;

public sealed class UserLockOutException : Exception, IBaseException
{
	public UserLockOutException(string message) : base(message)
	{
		ErrorMessage = message;
	}

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}