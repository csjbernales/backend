using Microsoft.Data.SqlClient;

namespace backend.data.Data
{
    public static class DbConnectionStringsBuilder
    {
        public static SqlConnection ConnectionBuilder(IConfigurationSection conn)
        {
            string dbConnectionString =
                    $"Data Source={conn["DataSource"]!};" +
                    $"Database={conn["Database"]!};" +
                    $"Integrated Security={bool.Parse(conn["IntegratedSecurity"]!)};" +
                    $"Connect Timeout={int.Parse(conn["ConnectTimeout"]!)};" +
                    $"Encrypt={bool.Parse(conn["Encrypt"]!)};" +
                    $"Trust Server Certificate={bool.Parse(conn["TrustServerCertificate"]!)};" +
                    $"Application Intent={conn["ApplicationIntent"]!};" +
                    $"Multi Subnet Failover={bool.Parse(conn["MultiSubnetFailover"]!)};";

            SqlConnection connection = new(dbConnectionString);
            return connection;
        }
    }
}