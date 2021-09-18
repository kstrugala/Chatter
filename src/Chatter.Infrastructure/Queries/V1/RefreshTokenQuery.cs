using Chatter.Infrastructure.CQRS.Queries;
using Chatter.Infrastructure.Responses.V1;

namespace Chatter.Infrastructure.Queries.V1
{
    public class RefreshTokenQuery : IQuery<JsonWebTokenDto>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
