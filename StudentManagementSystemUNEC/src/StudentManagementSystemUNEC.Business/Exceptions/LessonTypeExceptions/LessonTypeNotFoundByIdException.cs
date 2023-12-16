using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.LessonTypeExceptions;

public sealed class LessonTypeNotFoundByIdException : Exception, IBaseException
{
	public LessonTypeNotFoundByIdException(string message) : base(message)
	{
		ErrorMessage = message;
	}

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
