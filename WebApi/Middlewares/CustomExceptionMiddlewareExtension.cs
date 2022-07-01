using Microsoft.AspNetCore.Builder;

namespace WebApi.Middlewares;

public static class CustomExceptionMiddlewareExtension 
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}