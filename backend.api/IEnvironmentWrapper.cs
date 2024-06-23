namespace backend.api
{
    public interface IEnvironmentWrapper
    {
        bool IsDevelopment();
        bool IsProduction();
        bool IsStaging();
    }
}