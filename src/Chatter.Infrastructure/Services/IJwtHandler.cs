using Chatter.Core.Entities;
using System.Security.Claims;

namespace Chatter.Infrastructure.Services
{
    public interface IJwtHandler
    {
        /// <summary>
        /// Creates a new JWT token.
        /// </summary>
        /// <param name="email">User's email address</param>
        /// <param name="role">User's role</param>
        /// <returns></returns>
        JsonWebToken Create(string email, string role);

        /// <summary>
        /// Validates provided expired JWT token. It will return true only if the token is valid (issued by the app) and has expired.
        /// In other cases (invalid token, valid token that hasn't expired yet) it will return false.
        /// </summary>
        /// <param name="token">JWT token</param>
        /// <param name="claimsPrincipal">Claims Principal</param>
        /// <returns>True when provided token has expired and is valid otherwise false</returns>
        bool ValidateExpiredToken(string token, out ClaimsPrincipal claimsPrincipal);
    }
}
