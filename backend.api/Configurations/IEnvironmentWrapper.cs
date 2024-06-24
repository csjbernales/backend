namespace backend.api.Configurations
{
    public interface IEnvironmentWrapper
    {
        bool IsDevelopment();
        bool IsProduction();
        bool IsStaging();
    }
}