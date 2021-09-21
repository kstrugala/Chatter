using Chatter.Infrastructure.CQRS.Commands;
using System.Text.Json.Serialization;

namespace Chatter.Infrastructure.Commands.V1.Users
{
    public class ChangePasswordCommand : ICommand
    {
        [JsonIgnore]
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
