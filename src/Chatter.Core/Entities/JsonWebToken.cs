using System;

namespace Chatter.Core.Entities
{
    public class JsonWebToken
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
