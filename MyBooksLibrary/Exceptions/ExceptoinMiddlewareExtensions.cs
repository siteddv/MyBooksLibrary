using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using MyBooksLibrary.Data.ViewModels;
using System.Net;

namespace MyBooksLibrary.Exceptions
{
    public static class ExceptoinMiddlewareExtensions
    {
        public static void ConfigureBuildInExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var logger = loggerFactory.CreateLogger("ConfigureBuildInExceptionHandler");

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var contextRequest = context.Features.Get<IHttpRequestFeature>();

                    if(contextFeature != null)
                    {
                        var errorVmString = new ErrorViewModel()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Path = contextRequest.Path
                        }.ToString();

                        logger.LogError(errorVmString);

                        await context.Response.WriteAsync(errorVmString);
                    }
                });
            });
        }

        public static void ConfigureCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
