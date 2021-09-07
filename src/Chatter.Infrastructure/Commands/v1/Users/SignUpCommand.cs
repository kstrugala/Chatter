using Chatter.Infrastructure.CQRS.Commands;

namespace Chatter.Infrastructure.Commands.v1.Users
{
    public class SignUpCommand : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
