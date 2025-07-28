using System.Text.Json;
using PodcastApp.DTO.Responses;

namespace PodcastApp.API.Middleware
{
    public class ResponseWrappingMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseWrappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.ToString().Contains("simulate-middleware-error"))
            {
                throw new Exception("Simulated exception from middleware");
            }
            var originalBodyStream = context.Response.Body;

            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context); // Execute pipeline

            memoryStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
            memoryStream.Seek(0, SeekOrigin.Begin);

            if (!context.Response.ContentType?.Contains("application/json") ?? true)
            {
                await memoryStream.CopyToAsync(originalBodyStream);
                return;
            }

            object? result;
            try
            {
                result = JsonSerializer.Deserialize<object>(responseBody, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            }
            catch
            {
                result = responseBody;
            }

            var wrappedResponse = new APIResponse<object>(result!);
            context.Response.Body = originalBodyStream;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(wrappedResponse);
            await context.Response.WriteAsync(json);
        }
    }
}
