using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.FacultyExceptions;

public sealed class FacultyNotFoundByIdException : Exception, IBaseException
{
	public FacultyNotFoundByIdException(string message) : base(message)
	{
		ErrorMessage = message;
	}

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
