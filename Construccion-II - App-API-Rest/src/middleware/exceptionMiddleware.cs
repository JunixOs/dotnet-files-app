using Construccion_II___App_API_Rest.Src.Exceptions;

namespace Construccion_II___App_API_Rest.Src.Films.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api-soap"))
            {
                await _next(context); // deja pasar SOAP
                return;
            }

            try
            {
                await _next(context);
            }
            catch (AppException ex)
            {
                context.Response.StatusCode = ex.HttpCode;

                await context.Response.WriteAsJsonAsync(new
                {
                    message = ex.Message , 
                    errorCode = ex.ErrorCode
                });

                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = "Error interno del servidor"
                });

                _logger.LogError(ex.Message);
            }
        }
    }
}