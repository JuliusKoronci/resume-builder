using System;
using System.Net;
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
        public async Task Get_Should_Return_ProfileList()
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

        [Fact]
        public async Task Get_Should_Return_Profile()
        {
            var response = await Client.GetAsync("/api/Profile/1");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var profile = JsonConvert.DeserializeObject<Profile>(content);

            Assert.Equal("jk@web-solutions.sk", profile.Email);
            Assert.Equal(1, profile.Id);

            // Auditable entity props
            Assert.NotEmpty(profile.CreatedBy);
            Assert.IsType<DateTime>(profile.Created);
            Assert.Null(profile.LastModifiedBy);
            Assert.Null(profile.LastModified);
        }

        [Fact]
        public async Task Get_Should_Return_NotFound_Profile()
        {
            var response = await Client.GetAsync("/api/Profile/2");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
