namespace backend.api.Middleware
{
    public class ExceptionHandler(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}