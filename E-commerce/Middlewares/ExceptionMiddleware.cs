using E_commerce.Errors;
using System.Net;
using System.Text.Json;

namespace E_commerce.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                //development
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;


                var response = _environment.IsDevelopment()?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString())
                    :new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);

                //if production => log exception in the database
            }
        }




    }
}
