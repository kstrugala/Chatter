using Chatter.Infrastructure.Queries.V1;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Chatter.Api.IntegrationTests.V1
{
    public class TestControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Test_WhenUserIsAuthenticated_ShouldReturnAuthorizeString()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("api/v1/test");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsStringAsync()).Should().Be("Authorized");
        }
    }
}
