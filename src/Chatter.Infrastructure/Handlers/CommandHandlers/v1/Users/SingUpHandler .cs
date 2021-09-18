using Chatter.Infrastructure.Commands.V1.Users;
using Chatter.Infrastructure.CQRS.Commands;
using Chatter.Infrastructure.Services;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Handlers.CommandHandlers.V1.Users
{
    public class SignInHandler : ICommandHandler<SignUpCommand>
    {
        private readonly IUserService _userService;

        public SignInHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(SignUpCommand command)
            => await _userService.SignUpAsync(command.Email,
                                         command.Password,
                                         command.FirstName,
                                         command.LastName);
    }
}
