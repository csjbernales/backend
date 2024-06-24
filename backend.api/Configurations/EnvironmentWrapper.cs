namespace backend.api.Configurations
{
    public class EnvironmentWrapper(WebApplicationBuilder builder) : IEnvironmentWrapper
    {
        public virtual bool IsDevelopment()
        {
            return builder.Environment.IsDevelopment();
        }
        public virtual bool IsStaging()
        {
            return builder.Environment.IsStaging();
        }

        public virtual bool IsProduction()
        {
            return builder.Environment.IsProduction();
        }
    }
}