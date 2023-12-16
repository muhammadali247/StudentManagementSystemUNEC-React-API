using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.SubjectExceptions;

public sealed class SubjectNotFoundByIdException : Exception, IBaseException
{
	public SubjectNotFoundByIdException(string message) : base(message)
	{
		ErrorMessage = message;
	}

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
