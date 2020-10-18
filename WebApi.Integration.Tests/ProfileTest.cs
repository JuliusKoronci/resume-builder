using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Newtonsoft.Json;
using WebApi.Integration.Tests.Setup;
using Xunit;
using Xunit.Priority;

namespace WebApi.Integration.Tests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class ProfileTest : BaseTest
    {
        public ProfileTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        [Priority(1)]
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
        [Priority(2)]
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
        [Priority(3)]
        public async Task Get_Should_Return_NotFound_Profile()
        {
            var response = await Client.GetAsync("/api/Profile/2");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        [Priority(4)]
        public async Task Post_Should_Create_Profile()
        {
            var postObj = new Profile
            {
                Id = 2,
                Email = "jk2@web-solutions.sk",
                FirstName = "Julius2",
                LastName = "Koronci2",
                PhoneNumber = "0767654430",
                WebSite = "www.web-solutions.sk"
            };
            var response = await Client.PostAsync("/api/profile",
                new StringContent(JsonConvert.SerializeObject(postObj), Encoding.UTF8)
                {
                    Headers = {ContentType = new MediaTypeHeaderValue("application/json")}
                });

            response.EnsureSuccessStatusCode();

            var profile = JsonConvert.DeserializeObject<Profile>(await response.Content.ReadAsStringAsync());
            AuditableTest.EnsureNotModifiedAuditableEntity(profile);
            AuditableTest.EqualsIgnoreAuditableProps(profile, postObj);
        }

        [Fact]
        [Priority(5)]
        public async Task Put_Should_Update_Profile()
        {
            var postObj = new Profile
            {
                Id = 1,
                Email = "jk2@web-solutions.sk",
                FirstName = "Julius2",
                LastName = "Koronci2",
                PhoneNumber = "0767654430",
                WebSite = "www.web-solutions.sk"
            };

            var response = await Client.PutAsync("/api/profile/1",
                new StringContent(JsonConvert.SerializeObject(postObj), Encoding.UTF8)
                {
                    Headers = {ContentType = new MediaTypeHeaderValue("application/json")}
                });

            response.EnsureSuccessStatusCode();

            var updatedProfileResponse = await Client.GetAsync("/api/Profile/1");

            var profile =
                JsonConvert.DeserializeObject<Profile>(await updatedProfileResponse.Content.ReadAsStringAsync());
            AuditableTest.EnsureModifiedAuditableEntity(profile);
            AuditableTest.EqualsIgnoreAuditableProps(profile, postObj);
        }

        [Fact]
        [Priority(5)]
        public async Task Put_Should_Return_NotFound_Profile()
        {
            var postObj = new Profile
            {
                Id = 555,
                Email = "jk2@web-solutions.sk",
                FirstName = "Julius2",
                LastName = "Koronci2",
                PhoneNumber = "0767654430",
                WebSite = "www.web-solutions.sk"
            };

            var response = await Client.PutAsync("/api/profile/555",
                new StringContent(JsonConvert.SerializeObject(postObj), Encoding.UTF8)
                {
                    Headers = {ContentType = new MediaTypeHeaderValue("application/json")}
                });

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        [Priority(6)]
        public async Task Delete_Should_Remove_Profile()
        {
            var profiles = await Client.GetAsync("/api/Profile");
            var profileList = JsonConvert.DeserializeObject<Profile[]>(await profiles.Content.ReadAsStringAsync());
            Assert.Equal(2, profileList.Length);
            var response = await Client.DeleteAsync("/api/profile/2");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var profilesAfterDelete = await Client.GetAsync("/api/Profile");
            var profileListAfterDelete =
                JsonConvert.DeserializeObject<Profile[]>(await profilesAfterDelete.Content.ReadAsStringAsync());
            Assert.Single(profileListAfterDelete);
        }

        [Fact]
        [Priority(7)]
        public async Task Delete_Should_Return_NotFound_Profile()
        {
            var response = await Client.DeleteAsync("/api/profile/222");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
