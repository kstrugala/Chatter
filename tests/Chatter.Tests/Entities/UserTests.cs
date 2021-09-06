using Chatter.Core.Entities;
using Chatter.Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace Chatter.Tests.Entities
{
    public class UserTests
    {
        private readonly User _user;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserTests()
        {
            // Arrange
            _user = new User("test@test.com", Role.User);
            _passwordHasher = new PasswordHasher<User>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void When_first_name_is_null_or_whitespace_SetFirstName_method_should_throw_DomainException(string firstName)
        {
            Assert.Throws<DomainException>(() =>
            {
                // Act & Assert
                _user.SetFirstName(firstName);
            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void When_last_name_is_null_or_whitespace_SetLastName_method_should_throw_DomainException(string lastName)
        {
            Assert.Throws<DomainException>(() =>
            {
                // Act & Assert
                _user.SetLastName(lastName);
            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void When_password_is_null_or_whitespace_SetPassword_method_should_throw_DomainException(string password)
        {
            Assert.Throws<DomainException>(() =>
            {
                // Act & Assert
                _user.SetPassword(password, _passwordHasher);
            });
        }
    }
}