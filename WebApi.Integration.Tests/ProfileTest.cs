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

            AuditableTest.EnsureNotModifiedAuditableEntity(profileList[0]);
            AuditableTest.EqualsIgnoreAuditableProps(profileList[0], Utilities.GetTestProfile());
        }

        [Fact]
        public async Task Get_Should_Return_Profile()
        {
            var response = await Client.GetAsync("/api/Profile/1");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var profile = JsonConvert.DeserializeObject<Profile>(content);

            AuditableTest.EnsureNotModifiedAuditableEntity(profile);
            AuditableTest.EqualsIgnoreAuditableProps(profile, Utilities.GetTestProfile());
        }

        [Fact]
        public async Task Get_Should_Return_NotFound_Profile()
        {
            var response = await Client.GetAsync("/api/Profile/2");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
