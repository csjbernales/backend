namespace backend.api.Middleware.Interfaces
{
    public interface IInterceptor
    {
        Task Invoke(HttpContext context);
    }
}