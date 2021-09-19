using Chatter.Infrastructure.Commands.V1.Users;
using Chatter.Infrastructure.EF;
using Chatter.Infrastructure.Queries.V1;
using Chatter.Infrastructure.Responses.V1;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Chatter.Api.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                typeof(DbContextOptions<ChatterContext>));

                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }

                        services.AddDbContext<ChatterContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    });
                });

            TestClient = appFactory.CreateClient();
        }

        protected async Task SignUpUserAsync(string email, string firstName, string lastName, string password)
        {
            await TestClient.PostAsJsonAsync("api/v1/sign-up", new SignUpCommand
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password
            });
        }

        protected async Task<JsonWebTokenDto> SignInUserAsync(string email, string password)
        {
            var response = await TestClient.PostAsJsonAsync("api/v1/sign-in", new SignInQuery
            {
                Email = email,
                Password = password
            });

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<JsonWebTokenDto>();

            return null;
        }

        protected async Task AuthenticateAsync()
          => TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());

        private async Task<string> GetJwtAsync()
        {
            await SignUpUserAsync("test1@integration.com", "John", "Doe", "Pa55w.rd");
            var tokens = await SignInUserAsync("test1@integration.com", "Pa55w.rd");

            return tokens.Token;
        }


    }
}
