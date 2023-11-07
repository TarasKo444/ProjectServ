using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using ProjectServ.Application.Common;

namespace ProjectServ.WebApi.Middlewares;

[AllowAnonymous]
public class CustomExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException e)
        {
            await HandleValidationExceptionAsync(context, e);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int code = 500;
        string message = exception.Message;
        
        switch (exception)
        {
            case UserFriendlyException e:
                code = e.Status;
                message = e.Message;
                break;
        }

        context.Response.StatusCode = code;
        await context.Response.WriteAsJsonAsync(new { Status = code, Error = message });
    }
    private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
    {
        const int code = 400;
        context.Response.StatusCode = code;
        await context.Response.WriteAsJsonAsync(new { Status = code, Errors = exception.Errors.Select(e => e.ErrorMessage) });
    }
}

public static class CustomExtensionHandlerExtension
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CustomExceptionMiddleware>();
    }
}