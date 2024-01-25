using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using TechDrop.Logic.Exceptions;

namespace TechDrop.WebApi.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
            appError.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = MediaTypeNames.Application.Json;
                switch (exception)
                {
                    case UnauthorizedAccessException _:
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await context.Response.WriteAsync(JsonSerializer.Serialize(exception.Message));
                        break;
                    
                    case InternalServerException isEx:
                        context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = isEx.Code,
                            ErrorMessage = isEx.Message
                        }.ToString());
                        break;
                    
                    case NotAllowedException naEx:
                        context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = naEx.Code,
                            ErrorMessage = naEx.Message
                        }.ToString());
                        break;
                    
                    case NotFoundException nfEx:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = nfEx.Code,
                            ErrorMessage = nfEx.Message
                        }.ToString());
                        break;
                    
                    case UnauthorizedException unaEx:
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = unaEx.Code,
                            ErrorMessage = unaEx.Message
                        }.ToString());
                        break;
                    
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            ErrorMessage = exception?.Message ?? "Неизвестная ошибка."
                        }.ToString());
                        break;
                }
            })
        );
    }
}

public class ErrorDetails
{
    public int StatusCode { get; set; }

    public string? ErrorMessage { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}