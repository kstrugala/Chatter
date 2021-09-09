using Chatter.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace Chatter.Api.Extensions
{
    public static class ChatterExceptionHandlerExtensions
    {
        public static void UseChatterExceptionHandler(this IApplicationBuilder app)
            => app.UseExceptionHandler(c => c.Run(async context =>
             {
                 var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;

                 var errorCode = "error";
                 var statusCode = HttpStatusCode.BadRequest;
                 var exceptionType = exception.GetType();


                 switch (exception)
                 {
                     case UnauthorizedAccessException e when exceptionType == typeof(UnauthorizedAccessException):
                         statusCode = HttpStatusCode.Unauthorized;
                         break;
                     case ServiceException e when exceptionType == typeof(ServiceException):
                         statusCode = HttpStatusCode.BadRequest;
                         errorCode = e.Code;
                         break;
                     case Exception e when exceptionType == typeof(Exception):
                         statusCode = HttpStatusCode.InternalServerError;
                         break;
                 }

                 var response = new { errorCode = errorCode, errorMessage = exception.Message };

                 context.Response.StatusCode = (int)statusCode;
                 await context.Response.WriteAsJsonAsync(response);
             }));

    }
}
