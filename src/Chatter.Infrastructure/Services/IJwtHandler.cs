using Chatter.Core.Entities;

namespace Chatter.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JsonWebToken Create(string email, string role);
    }
}
