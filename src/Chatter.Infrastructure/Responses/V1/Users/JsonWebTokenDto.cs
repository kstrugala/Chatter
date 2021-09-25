using System;

namespace Chatter.Infrastructure.Responses.V1.Users
{
    public class JsonWebTokenDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }
    }
}
