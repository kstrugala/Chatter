using Chatter.Core.Entities;
using Chatter.Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace Chatter.Core.UnitTests.Entities
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
        public void SetFirstName_ShouldThrowDomainException_WhenFirstNameIsNullOrWhitespace(string firstName)
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
        public void SetLastName_ShouldThrowDomainException_WhenLastNameIsNullOrWhitespace(string lastName)
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
        public void SetPassword_ShouldThrowDomainException_WhenPasswordIsNullOrWhitespace(string password)
        {
            Assert.Throws<DomainException>(() =>
            {
                // Act & Assert
                _user.SetPassword(password, _passwordHasher);
            });
        }
    }
}
