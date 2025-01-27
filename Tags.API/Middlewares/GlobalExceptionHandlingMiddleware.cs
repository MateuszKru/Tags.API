using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Tags.API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (HttpRequestException e)
            {
                logger.LogError(e, e.Message);

                context.Response.StatusCode = e.StatusCode != null ? (int)e.StatusCode : StatusCodes.Status500InternalServerError;

                ProblemDetails problem = new()
                {
                    Status = context.Response.StatusCode,
                    Type = "SO API error",
                    Title = "Error while fetch data from SO API.",
                    Detail = e.Message,
                };

                string json = JsonConvert.SerializeObject(problem);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                ProblemDetails problem = new()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Type = "Server error",
                    Title = "Server error",
                    Detail = "An internal server has occurred",
                };

                string json = JsonConvert.SerializeObject(problem);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
    }
}