namespace backend.api.Models
{
    /// <summary>
    /// ConnectionStrings
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// DataSource
        /// </summary>
        public string DataSource { get; set; } = string.Empty;

        /// <summary>
        /// Database
        /// </summary>

        public string Database { get; set; } = string.Empty;

        /// <summary>
        /// IntegratedSecurity
        /// </summary>

        public bool IntegratedSecurity { get; set; }

        /// <summary>
        /// ConnectTimeout
        /// </summary>

        public int ConnectTimeout { get; set; }

        /// <summary>
        /// Encrypt
        /// </summary>

        public bool Encrypt { get; set; }

        /// <summary>
        /// TrustServerCertificate
        /// </summary>

        public bool TrustServerCertificate { get; set; }

        /// <summary>
        /// ApplicationIntent
        /// </summary>

        public string ApplicationIntent { get; set; } = string.Empty;

        /// <summary>
        /// MultiSubnetFailover
        /// </summary>
        public bool MultiSubnetFailover { get; set; }
    }
}