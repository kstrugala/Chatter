using Chatter.Infrastructure.CQRS.Queries;
using Chatter.Infrastructure.Responses.V1;

namespace Chatter.Infrastructure.Queries.V1
{
    public class SignInQuery : IQuery<JsonWebTokenDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
