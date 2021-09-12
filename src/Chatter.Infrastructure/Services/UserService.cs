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
        private readonly IJwtHandler _jwtHandler;

        public UserService(ChatterContext context, IPasswordHasher<User> passwordHasher, IJwtHandler jwtHandler)
        {
            _context=context;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
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

        public async Task<JsonWebToken> SignIn(string email, string password)
        {
            var user = await GetUser(email);

            if (user == null)
                throw new ServiceException(Error.InvalidCredentials, "Invalid credentials");

            if(!user.ValidatePassword(password, _passwordHasher))
                throw new ServiceException(Error.InvalidCredentials, "Invalid credentials");

            return _jwtHandler.Create(user.Email, user.Role);
        }

        private async Task<User> GetUser(string email)
          => await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
    }
}
