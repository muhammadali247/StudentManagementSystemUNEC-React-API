using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.GroupSubjectExceptions;

public sealed  class GroupSubjectNotFoundByIdException : Exception, IBaseException
{
	public GroupSubjectNotFoundByIdException(string message) : base(message)
	{
		ErrorMessage = message;
	}

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}