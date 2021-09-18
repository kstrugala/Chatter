using Chatter.Core.Entities;
using Chatter.Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Chatter.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _validationParameters;

        public JwtHandler(JwtSettings jwtSettings, TokenValidationParameters validationParameters)
        {
            _jwtSettings = jwtSettings;
            _validationParameters = validationParameters;
        }

        /// <summary>
        /// Creates a new JWT token.
        /// </summary>
        /// <param name="email">User's email address</param>
        /// <param name="role">User's role</param>
        /// <returns></returns>
        public JsonWebToken Create(string email, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(JwtRegisteredClaimNames.Email, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JsonWebToken
            {
                Token = tokenHandler.WriteToken(token),
                Jti = tokenDescriptor.Subject.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value,
                Expires = token.ValidTo
            };
        }

        /// <summary>
        /// Validates provided expired JWT token. It will return true only if the token is valid (issued by the app) and has expired.
        /// In other cases (invalid token, valid token that hasn't expired yet) it will return false.
        /// </summary>
        /// <param name="token">JWT token</param>
        /// <param name="claimsPrincipal">Claims Principal</param>
        /// <returns>True when provided token has expired and is valid otherwise false</returns>
        public bool ValidateExpiredToken(string token, out ClaimsPrincipal claimsPrincipal)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = _validationParameters.Clone();
            validationParameters.ValidateLifetime = false;

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                claimsPrincipal = principal;

                if (validatedToken is null ||
                    !IsValidJwtSecurityAlgorithm(validatedToken) ||
                    !HasJwtTokenExpired(principal))
                    return false;


                return true;
            }
            catch
            {
                // If during the validation any exception will be thrown it means that the token is invalid
                claimsPrincipal = null;
                return false;
            }
        }


        private bool IsValidJwtSecurityAlgorithm(SecurityToken token)
            => (token is JwtSecurityToken jwtSecurityToken) &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256);

        private bool HasJwtTokenExpired(ClaimsPrincipal principal)
        {
            var expiryDateEpoch = long.Parse(principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateEpoch);

            if (DateTime.UtcNow > expiryDateTimeUtc)
                return true;

            return false;
        }
    }
}
