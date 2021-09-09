using Chatter.Core.Entities;
using Chatter.Core.ErrorCodes.V1;
using Chatter.Infrastructure.EF;
using Chatter.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ChatterContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(ChatterContext context, IPasswordHasher<User> passwordHasher)
        {
            _context=context;
            _passwordHasher = passwordHasher;
        }

        public async Task SignUp(string email, string password, string firstName, string lastName)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (user != null)
                throw new ServiceException(Error.InvalidEmail, $"User with email:{email} already exists.");

            user = new User(email, Role.User);
            user.SetPassword(password, _passwordHasher);
            user.SetFirstName(firstName);
            user.SetLastName(lastName);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
