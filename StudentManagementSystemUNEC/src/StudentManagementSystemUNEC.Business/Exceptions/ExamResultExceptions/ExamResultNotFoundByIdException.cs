using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.ExamResultExceptions;

public sealed class ExamResultNotFoundByIdException : Exception, IBaseException
{
	public ExamResultNotFoundByIdException(string message) : base(message)
	{
		ErrorMessage = message;
	}

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}