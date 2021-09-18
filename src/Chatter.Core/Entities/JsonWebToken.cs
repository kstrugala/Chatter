using System;

namespace Chatter.Core.Entities
{
    public class JsonWebToken
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Jti { get; set; }
        public DateTime Expires { get; set; }
    }
}
