using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.ExamExceptions;

public sealed class ExamNotFoundByIdException : Exception, IBaseException
{
	public ExamNotFoundByIdException(string message) : base(message)
	{
		ErrorMessage = message;
	}

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}