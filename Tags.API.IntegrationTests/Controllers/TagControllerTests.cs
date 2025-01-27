using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Tags.Infrastructure.Persistence;
using Xunit;

namespace Tags.API.IntegrationTests.Controllers
{
    public class TagControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services
                         .Single(service => service.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                        services.Remove(dbContextOptions);

                        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TagsDb"));
                    });
                })
                .CreateClient();

        [Fact()]
        public async Task GetTest()
        {
            // arrange

            // act

            var response = await _client.GetAsync("/tag");

            // assert

            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact()]
        public async Task DownloadTagsTest()
        {
            // arrange

            // act

            var response = await _client.GetAsync("/tag/downloadTags");

            // assert

            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}