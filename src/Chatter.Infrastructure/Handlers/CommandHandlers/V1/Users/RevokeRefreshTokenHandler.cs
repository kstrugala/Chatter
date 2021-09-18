using Chatter.Infrastructure.Commands.V1.Users;
using Chatter.Infrastructure.CQRS.Commands;
using Chatter.Infrastructure.Services;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Handlers.CommandHandlers.V1.Users
{
    public class RevokeRefreshTokenHandler : ICommandHandler<RevokeRefreshTokenCommand>
    {
        private readonly IUserService _userService;

        public RevokeRefreshTokenHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(RevokeRefreshTokenCommand command)
            => await _userService.RevokeRefreshTokenAsync(command.RefreshToken);
    }
}
