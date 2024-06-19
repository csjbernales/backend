namespace backend.api.Models
{
             public class ConnectionStrings
    {
                             public string DataSource { get; set; } = string.Empty;

                     
        public string Database { get; set; } = string.Empty;

                     
        public bool IntegratedSecurity { get; set; }

                     
        public int ConnectTimeout { get; set; }

                     
        public bool Encrypt { get; set; }

                     
        public bool TrustServerCertificate { get; set; }

                     
        public string ApplicationIntent { get; set; } = string.Empty;

                             public bool MultiSubnetFailover { get; set; }
    }
}