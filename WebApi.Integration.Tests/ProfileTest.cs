using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using WebApi.Integration.Tests.Setup;
using Xunit;

namespace WebApi.Integration.Tests
{
    public class ProfileTest : IClassFixture<CustomWebApplicationFactory<WebApi.Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public ProfileTest(CustomWebApplicationFactory<WebApi.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Get_ProfileList()
        {
            var response = await _client.GetAsync("/api/Profile");
            
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var profileList = JsonConvert.DeserializeObject<Profile[]>(content);
            
            Assert.Single(profileList);
            Assert.Equal("jk@web-solutions.sk", profileList[0].Email);
        }
    }
}
