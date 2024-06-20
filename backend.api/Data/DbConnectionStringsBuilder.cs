using backend.api.Models;
using Microsoft.Data.SqlClient;

namespace backend.api.Data
{
    public static class DbConnectionStringsBuilder
    {
        public static string DBConnectionString { get; set; } = null!;

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

            SqlConnection connection = new(ConnectionString(dbProps));
            return connection;
        }

        public static string ConnectionString(ConnectionStrings connectionStrings)
        {
            if (connectionStrings is not null)
            {
                string dataSource = connectionStrings.DataSource;
                string database = connectionStrings.Database;
                string integratedSecurity = connectionStrings.IntegratedSecurity.ToString();
                string connectTimeout = connectionStrings.ConnectTimeout.ToString();

                string encrypt = connectionStrings.Encrypt.ToString();
                string trustServerCertificate = connectionStrings.TrustServerCertificate.ToString();
                string applicationIntent = connectionStrings.ApplicationIntent;
                string multiSubnetFailover = connectionStrings.MultiSubnetFailover.ToString();

                DBConnectionString = $"Data Source={dataSource};" +
                    $"Database={database};" +
                    $"Integrated Security={integratedSecurity};" +
                    $"Connect Timeout={connectTimeout};" +

                    $"Encrypt={encrypt};" +
                    $"Trust Server Certificate={trustServerCertificate};" +
                    $"Application Intent={applicationIntent};" +
                    $"Multi Subnet Failover={multiSubnetFailover};";

                return DBConnectionString;
            }
            return string.Empty;
        }
    }
}