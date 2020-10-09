using System;
using System.Threading.Tasks;
using Domain.Entities;
using Newtonsoft.Json;
using WebApi.Integration.Tests.Setup;
using Xunit;

namespace WebApi.Integration.Tests
{
    public class ProfileTest : BaseTest
    {
        public ProfileTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Get_ProfileList()
        {
            var response = await Client.GetAsync("/api/Profile");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var profileList = JsonConvert.DeserializeObject<Profile[]>(content);

            Assert.Single(profileList);
            Assert.Equal("jk@web-solutions.sk", profileList[0].Email);

            // Auditable entity props
            Assert.NotEmpty(profileList[0].CreatedBy);
            Assert.IsType<DateTime>(profileList[0].Created);
            Assert.Null(profileList[0].LastModifiedBy);
            Assert.Null(profileList[0].LastModified);
        }
    }
}
