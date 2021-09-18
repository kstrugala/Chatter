using Chatter.Infrastructure.CQRS.Commands;

namespace Chatter.Infrastructure.Commands.V1.Users
{
    public class RevokeRefreshTokenCommand : ICommand
    {
        public string RefreshToken { get; set; }
    }
}
