using System;

namespace Chatter.Core.Entities
{
    public class RefreshToken
    {
        public string Token { get; private set; }
        public string JwtId { get; private set; }

        public int UserId { get; private set; }
        public User User { get; private set; }

        public bool Used { get; private set; }
        public bool Revoked { get; private set; }

        public DateTime CreationDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }

        protected RefreshToken()
        {

        }

        public RefreshToken(string token, string jwtId,  User user, int expiryMonths)
        {
            Token = token;
            JwtId = jwtId;
            User = user;

            Used = false;
            Revoked = false;

            CreationDate = DateTime.UtcNow;
            ExpirationDate = DateTime.UtcNow.AddMonths(expiryMonths);
        }

        public void Use()
            => Used = true;

        public void Revoke()
            => Revoked = true;
    }
}
