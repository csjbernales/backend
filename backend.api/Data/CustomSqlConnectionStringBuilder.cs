using backend.api.Models;

namespace backend.api.Data
{
    public class CustomSqlConnectionStringBuilder(ConnectionStrings connectionStrings) : ICustomSqlConnectionStringBuilder
    {
        public string ConnectionString()
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

                return $"Data Source={dataSource};" +
                    $"Database={database};" +
                    $"Integrated Security={integratedSecurity};" +
                    $"Connect Timeout={connectTimeout};" +

                    $"Encrypt={encrypt};" +
                    $"Trust Server Certificate={trustServerCertificate};" +
                    $"Application Intent={applicationIntent};" +
                    $"Multi Subnet Failover={multiSubnetFailover};";
            }

            return string.Empty;
        }
    }
}