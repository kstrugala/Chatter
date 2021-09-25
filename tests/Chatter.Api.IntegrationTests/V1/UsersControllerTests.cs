using Chatter.Infrastructure.Commands.V1.Users;
using Chatter.Infrastructure.Queries.V1;
using Chatter.Infrastructure.Queries.V1.Users;
using Chatter.Infrastructure.Responses.V1.Users;
using FluentAssertions;
using FluentAssertions.Extensions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Chatter.Api.IntegrationTests.V1
{
    public class UsersControllerTests : IntegrationTest
    {
        [Fact]
        public async Task SignIn_WhenUserExist_ShouldReturnTokensAndProperJwtTokenExpirationDate()
        {
            // Arrange
            await SignUpUserAsync("test1@test.com", "Jane", "Doe", "Pa55w.rd");

            // Act
            var response = await TestClient.PostAsJsonAsync("api/v1/sign-in", new SignInQuery
            {
                Email = "test1@test.com",
                Password = "Pa55w.rd"
            });


            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsAsync<JsonWebTokenDto>();
            
            content.Token.Should().NotBeNullOrWhiteSpace();
            content.RefreshToken.Should().NotBeNullOrWhiteSpace();
            content.Expires.Should().BeWithin(5.Minutes()).After(DateTime.UtcNow);
        }

        [Fact]
        public async Task SignUp_WhenUserAlreadyExist_ShouldResponseWithStatusBadRequest()
        {
            // Arrange
            await SignUpUserAsync("test1@test.com", "Jane", "Doe", "Pa55w.rd");

            // Act

            var response = await TestClient.PostAsJsonAsync("api/v1/sign-up", new SignUpCommand
            {
                Email = "test1@test.com",
                FirstName = "Jane",
                LastName = "Doe",
                Password = "Pa55w.rd"
            });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task SignUp_WhenUserDoesntExist_ShouldResponseWithStatusNoContent()
        {
            // Arrange

            // Act

            var response = await TestClient.PostAsJsonAsync("api/v1/sign-up", new SignUpCommand
            {
                Email = "test1@test.com",
                FirstName = "Jane",
                LastName = "Doe",
                Password = "Pa55w.rd"
            });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}