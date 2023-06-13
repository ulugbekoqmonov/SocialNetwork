using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace WebUI.GlobalExeptionHandler;

public class GlobalExeptionHandler
{
    private readonly RequestDelegate _next;

    public GlobalExeptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exp)
        {
            Log.Error("Exeption logger");
            await HandleExceptionMessageAsync(httpContext, exp);
        }
    }
    private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        int statusCode = (int)HttpStatusCode.InternalServerError;
        var result = JsonConvert.SerializeObject(new
        {
            StatusCode = statusCode,
            ErrorMessage = exception.Message + " catch in Exeption handler"
        });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(result);
    }
}

public static class GlobalExeptionHandlerExtensions
{
    public static IApplicationBuilder UseGlobalExeptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExeptionHandler>();
    }
}