using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.SubjectExceptions;

public sealed  class SubjectIsAlreadyExistException : Exception, IBaseException
{
	public SubjectIsAlreadyExistException(string message) : base(message)
	{
		ErrorMessage = message;
	}

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}
