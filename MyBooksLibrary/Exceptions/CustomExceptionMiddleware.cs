using Microsoft.AspNetCore.Http;
using MyBooksLibrary.Data.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MyBooksLibrary.Exceptions
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new ErrorViewModel()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = "Internal Server Error is handled by custom middleware",
                Path = "path"
            };

            return httpContext.Response.WriteAsync(response.ToString());
        }
    }
}
