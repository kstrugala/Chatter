using Chatter.Core.Entities;
using Chatter.Infrastructure.EF;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ChatterContext _context;
        public UserService(ChatterContext context, IPasswordHasher<User> passwordHasher)
        {
            _context=context;
        }

        public Task SignUp(string email, string password, string firsName, string lastName)
        {
            throw new System.NotImplementedException();
        }
    }
}
