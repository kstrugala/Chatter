using Chatter.Core.Entities;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task SignUpAsync(string email, string password, string firstName, string lastName);
        Task<JsonWebToken> SignInAsync(string email, string password);

        Task<JsonWebToken> RefreshAccessTokenAsync(string token, string refreshToken);
        Task RevokeRefreshTokenAsync(string token);
    }
}
