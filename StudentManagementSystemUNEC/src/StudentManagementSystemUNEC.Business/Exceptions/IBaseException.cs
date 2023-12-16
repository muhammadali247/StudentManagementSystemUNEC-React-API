using System.Net;

namespace StudentManagementSystemUNEC.Business.Exceptions;

public interface IBaseException
{
    HttpStatusCode StatusCode { get; }
    string ErrorMessage { get; }
}
