using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;

namespace Persistence.Seeds
{
    public class ProfileSeed : ISeed
    {
        private readonly IApplicationDbContext _context;

        public ProfileSeed(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedInitData()
        {
            await _context.SaveChangesAsync();
        }

        public async Task SeedSampleData()
        {
            if (!_context.Profiles.Any())
                _context.Profiles.Add(new Profile
                {
                    Email = "jk@web-solutions.sk",
                    FirstName = "Julius",
                    LastName = "Koronci",
                    PhoneNumber = "0767654430",
                    WebSite = "www.web-solutions.sk"
                });

            await _context.SaveChangesAsync();
        }
    }
}
