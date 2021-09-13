using Chatter.Core.Entities;
using Xunit;

namespace Chatter.Core.UnitTests.Entities
{
    public class RoleTests
    {
        [Fact]
        public void IsValid_ShouldReturnTrue_WhenRoleIsEqualToUser()
        {
            // Arrange
            var role = Role.User;
            // Act
            var result = Role.IsValid(role);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_ShouldReturnTrue_WhenRoleIsEqualToAdmin()
        {
            // Arrange
            var role = Role.Admin;
            // Act
            var result = Role.IsValid(role);
            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void IsValid_ShouldReturnFalse_WhenRoleIsNullOrWhitespace(string role)
        {
            // Arrange & Act
            var result = Role.IsValid(role);
            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("manager")]
        [InlineData("Moderator")]
        [InlineData("Administrator")]
        public void IsValid_ShouldReturnFalse_WhenRoleIsInvalid(string role)
        {
            // Arrange & Act
            var result = Role.IsValid(role);
            // Assert
            Assert.False(result);
        }
    }
}
