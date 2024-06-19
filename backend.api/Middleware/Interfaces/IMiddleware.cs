namespace backend.api.Middleware.Interfaces
{
    public interface IMiddleware
    {
        Task Invoke(HttpContext context);
    }
}