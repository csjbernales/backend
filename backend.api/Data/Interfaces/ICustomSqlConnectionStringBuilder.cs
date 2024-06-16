namespace backend.api.Data.Interfaces
{
    /// <summary>
    /// CustomSqlConnectionStringBuilder interface
    /// </summary>
    public interface ICustomSqlConnectionStringBuilder
    {
        /// <summary>
        /// Connection string builder
        /// </summary>
        /// <returns>Connection string built</returns>
        string ConnectionString();
    }
}