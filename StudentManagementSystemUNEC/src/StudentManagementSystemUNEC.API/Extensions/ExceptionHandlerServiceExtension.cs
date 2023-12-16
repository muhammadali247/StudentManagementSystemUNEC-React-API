using Microsoft.AspNetCore.Diagnostics;
using StudentManagementSystemUNEC.Business.DTOs.CommonDTOs;
using StudentManagementSystemUNEC.Business.Exceptions;
using System.Net;

namespace StudentManagementSystemUNEC.API.Extensions;

public static class ExceptionHandlerServiceExtension
{
    public static IApplicationBuilder AddExceptionHandler(this IApplicationBuilder application)
    {
        application .UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerFeature>();
                HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
                string message = "Unexcpected error ocurred!";

                //switch (feature.Error)
                //{
                //    case StudentNotFoundByIdException:
                //        statusCode = HttpStatusCode.NotFound;
                //        message = feature.Error.Message;
                //        break;
                //    default:
                //        break;
                //}
                if (feature.Error is IBaseException)
                {
                    var exception = (IBaseException)feature.Error;
                    statusCode = exception.StatusCode;
                    message = exception.ErrorMessage;
                }

                var response = new ResponseDTO(statusCode, message);

                context.Response.StatusCode = (int)statusCode;
                await context.Response.WriteAsJsonAsync(response);
                await context.Response.CompleteAsync();
            });
        });

        return application;
    }
}
