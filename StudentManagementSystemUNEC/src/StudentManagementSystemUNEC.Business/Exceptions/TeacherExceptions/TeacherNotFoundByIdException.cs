using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.TeacherExceptions;

public sealed class TeacherNotFoundByIdException : Exception, IBaseException
{
	public TeacherNotFoundByIdException(string message) : base(message)
	{
		ErrorMessage = message;
	}

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
