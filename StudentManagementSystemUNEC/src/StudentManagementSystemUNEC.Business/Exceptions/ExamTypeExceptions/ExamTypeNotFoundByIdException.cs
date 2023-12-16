using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.ExamTypeExceptions;

public sealed class ExamTypeNotFoundByIdException : Exception, IBaseException
{
	public ExamTypeNotFoundByIdException(string message) : base(message)
	{
		ErrorMessage = message;
	}

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}