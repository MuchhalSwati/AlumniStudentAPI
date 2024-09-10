using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using StudentsAdminPortal.API.Models;
using ValidationException = FluentValidation.ValidationException;


namespace StudentsAdminPortal.API.Exceptions
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch(error)
                {
                    case BadHttpRequestException e:
                    case ValidationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = (int)HttpStatusCode.BadRequest,
                            Message = "Bad Request exception"
                        }.ToString());
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = response.StatusCode,
                            Message = "Internal Server Error"
                        }.ToString());
                        break;

                }
            }
        }
    }
}
