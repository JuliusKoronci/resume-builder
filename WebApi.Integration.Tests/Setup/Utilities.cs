using System.Threading.Tasks;
using Domain.Entities;
using Persistence.Context;
using Persistence.Seeds;

namespace WebApi.Integration.Tests.Setup
{
    public static class Utilities
    {
        public static void InitializeDbForTests(ApplicationDbContext db)
        {
            db.Profiles.Add(new Profile()
            {
                Email = "jk@web-solutions.sk",
                FirstName = "Julius",
                LastName = "Koronci",
                PhoneNumber = "0767654430",
                WebSite = "www.web-solutions.sk",
            });

            db.SaveChangesAsync();
        }
    }
}
