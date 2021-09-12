using System;

namespace Chatter.Infrastructure.Responses.V1
{
    public class JsonWebTokenDto
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
