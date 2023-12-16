using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.GroupExceptions;

public sealed class GroupNotFoundByIdException : Exception, IBaseException
{
	public GroupNotFoundByIdException(string message) : base(message)
	{
		ErrorMessage = message;
	}

	public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

	public string ErrorMessage { get; }
}
