using Microsoft.AspNetCore.Authorization;

namespace backend.api.Auth
{
    /// <summary>
    /// HasScopeRequirement class
    /// </summary>
    /// <remarks>
    /// HasScopeRequirement constructor
    /// </remarks>
    /// <param name="scope">scope</param>
    /// <param name="issuer">issuer</param>
    /// <exception cref="ArgumentNullException"></exception>
    public class HasScopeRequirement(string scope, string issuer) : IAuthorizationRequirement
    {
        /// <summary>
        /// Issuer
        /// </summary>
        public string Issuer { get; } = issuer ?? throw new ArgumentNullException(nameof(issuer));
        /// <summary>
        /// Scope
        /// </summary>
        public string Scope { get; } = scope ?? throw new ArgumentNullException(nameof(scope));
    }
}
