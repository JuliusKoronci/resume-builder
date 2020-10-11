using Domain.Entities;
using Persistence.Context;

namespace WebApi.Integration.Tests.Setup
{
    public static class Utilities
    {
        public static Profile GetTestProfile()
        {
            return new Profile
            {
                Id = 1,
                Email = "jk@web-solutions.sk",
                FirstName = "Julius",
                LastName = "Koronci",
                PhoneNumber = "0767654430",
                WebSite = "www.web-solutions.sk"
            };
        }

        public static void InitializeDbForTests(ApplicationDbContext db)
        {
            db.Profiles.Add(GetTestProfile());

            db.SaveChangesAsync();
        }
    }
}
