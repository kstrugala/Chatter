using Chatter.Core.Entities;
using Chatter.Core.ErrorCodes.V1;
using Chatter.Infrastructure.EF;
using Chatter.Infrastructure.Exceptions;
using Chatter.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ChatterContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandler _jwtHandler;
        private readonly RefreshTokenSettings _refreshTokenSettings;

        public UserService(ChatterContext context, IPasswordHasher<User> passwordHasher, 
                           IJwtHandler jwtHandler, RefreshTokenSettings refreshTokenSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
            _refreshTokenSettings = refreshTokenSettings;
        }

        public async Task SignUpAsync(string email, string password, string firstName, string lastName)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (user != null)
                throw new ServiceException(Error.InvalidEmail, $"User with email:{email} already exists.");

            user = new User(email, Role.User);
            user.SetPassword(password, _passwordHasher);
            user.SetFirstName(firstName);
            user.SetLastName(lastName);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<JsonWebToken> SignInAsync(string email, string password)
        {
            var user = await GetUserAsync(email);

            if (user == null)
                throw new ServiceException(Error.InvalidCredentials, "Invalid credentials");

            if (!user.ValidatePassword(password, _passwordHasher))
                throw new ServiceException(Error.InvalidCredentials, "Invalid credentials");

            return await GenerateToken(user);
        }

        public async Task ChangePasswordAsync(string email, string oldPassword, string newPassword)
        {
            var exception =  new ServiceException(Error.InvalidCredentials, "Invalid credentials");

            var user = await GetUserAsync(email);

            if (user == null)
                throw exception;

            if (!user.ValidatePassword(oldPassword, _passwordHasher))
                throw exception;

            user.SetPassword(newPassword, _passwordHasher);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<JsonWebToken> RefreshAccessTokenAsync(string token, string refreshToken)
        {
            var exception = new ServiceException(Error.InvalidToken, "Invalid token or refresh token");

            var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(t => t.Token == refreshToken);

            if (_jwtHandler.ValidateExpiredToken(token, out var claimsPrincipal) == false)
                throw exception;

            var jti = claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            if (storedRefreshToken is null ||
                DateTime.UtcNow > storedRefreshToken.ExpirationDate ||
                storedRefreshToken.Used ||
                storedRefreshToken.Revoked ||
                storedRefreshToken.JwtId != jti)
                throw exception;

            storedRefreshToken.Use();
            _context.RefreshTokens.Update(storedRefreshToken);
            
            var email = claimsPrincipal.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
            var user = _context.Users.SingleOrDefault(u => u.Email == email);

            if(user is null)
                throw exception;
            
            return await GenerateToken(user);
        }

        public async Task RevokeRefreshTokenAsync(string token)
        {
            var refreshToken = await GetRefreshTokenAsync(token);

            if (refreshToken is null)
                throw new ServiceException(Error.InvalidToken, "Refresh token was not found or revoked");

            refreshToken.Revoke();

            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();
        }

        private async Task<User> GetUserAsync(string email)
            => await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

        private async Task<RefreshToken> GetRefreshTokenAsync(string token)
            => await _context.RefreshTokens.SingleOrDefaultAsync(r => r.Token == token);

        private async Task<RefreshToken> GetRefreshTokenAsync(string token, User user)
            => await _context.RefreshTokens.SingleOrDefaultAsync(r => r.Token == token && r.UserId == user.Id);

        private string GenerateRefreshTokenString()
           => Guid.NewGuid()
                    .ToString()
                    .Replace("-", string.Empty);

        private async Task<JsonWebToken> GenerateToken(User user)
        {
            var result = _jwtHandler.Create(user.Email, user.Role);

            var refreshToken = new RefreshToken(GenerateRefreshTokenString(),
                                                result.Jti,
                                                user,
                                                _refreshTokenSettings.ExpiryMonths);

            result.RefreshToken = refreshToken.Token;

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return result;
        }      
    }
}
