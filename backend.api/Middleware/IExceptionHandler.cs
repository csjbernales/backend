
namespace backend.api.Middleware
{
    public interface IExceptionHandler
    {
        Task Invoke(HttpContext context);
    }
}