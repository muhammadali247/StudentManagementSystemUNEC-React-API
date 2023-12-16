using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.ExamResultExceptions;

public sealed class ExamResultWrongScoreInputException : Exception, IBaseException
{
    public ExamResultWrongScoreInputException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}