namespace backend.api.Middleware.Interfaces
{
    public interface IExceptionHandler
    {
        Task Invoke(HttpContext context);
    }
}