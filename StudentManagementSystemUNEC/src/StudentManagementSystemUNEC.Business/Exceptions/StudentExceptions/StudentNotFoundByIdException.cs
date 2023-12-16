using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.StudentExceptions;

public sealed class StudentNotFoundByIdException : Exception, IBaseException
{
	public StudentNotFoundByIdException(string message) : base(message)
	{
		ErrorMessage = message;
	}

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}