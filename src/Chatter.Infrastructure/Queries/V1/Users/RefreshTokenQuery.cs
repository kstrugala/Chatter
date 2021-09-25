using Chatter.Infrastructure.CQRS.Queries;
using Chatter.Infrastructure.Responses.V1.Users;

namespace Chatter.Infrastructure.Queries.V1.Users
{
    public class RefreshTokenQuery : IQuery<JsonWebTokenDto>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
