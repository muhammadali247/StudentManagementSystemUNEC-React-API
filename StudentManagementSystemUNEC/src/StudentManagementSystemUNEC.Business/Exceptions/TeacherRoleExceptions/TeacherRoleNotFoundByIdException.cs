using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions.TeacherRoleExceptions;

public sealed class TeacherRoleNotFoundByIdException : Exception, IBaseException
{
	public TeacherRoleNotFoundByIdException(string message) : base(message)
	{
		ErrorMessage = message;
	}

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
