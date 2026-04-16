using API_SOAP.Src.Exceptions;

namespace API_SOAP.Src.Middleware
{
    internal class ExceptionMiddleware 
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
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
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(new
                {
                    message = "Error interno del servidor"
                });
                throw;
            }
        }
    }
}