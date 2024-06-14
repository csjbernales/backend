
namespace backend.api.Middleware
{
    public interface IInterceptor
    {
        Task Invoke(HttpContext context);
    }
}