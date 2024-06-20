using backend.api.Models;
using Microsoft.Data.SqlClient;

namespace backend.api.Data
{
    public static class DbConnectionStringsBuilder
    {
        public static SqlConnection ConnectionBuilder(IConfigurationSection conn)
        {
            ConnectionStrings dbProps = new()
            {
                DataSource = conn["DataSource"]!,
                Database = conn["Database"]!,
                IntegratedSecurity = bool.Parse(conn["IntegratedSecurity"]!),
                ConnectTimeout = int.Parse(conn["ConnectTimeout"]!),
                Encrypt = bool.Parse(conn["Encrypt"]!),
                TrustServerCertificate = bool.Parse(conn["TrustServerCertificate"]!),
                ApplicationIntent = conn["ApplicationIntent"]!,
                MultiSubnetFailover = bool.Parse(conn["MultiSubnetFailover"]!)
            };

            SqlConnection connection = new(new CustomSqlConnectionStringBuilder(dbProps).ConnectionString());
            return connection;
        }
    }
}
