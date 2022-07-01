using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;

namespace WebApi.Middlewares;

public class CustomExceptionMiddleware 
{
    private readonly RequestDelegate next;

    public CustomExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = Stopwatch.StartNew();

        try
        {
            string message = "[Request] HTTP "+ context.Request.Method + " - " + context.Request.Path;

            await next(context);
            watch.Stop();

            message = "[Response] HTTP "+ context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms ";
        }
        catch (Exception ex)
        {
            watch.Stop();
            await HandleException(context, ex, watch);
        }
    }

    private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        string message = "[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";

        var result = JsonConvert.SerializeObject(new {error = ex.Message}, Formatting.None);

        return context.Response.WriteAsync(result);
    }


}