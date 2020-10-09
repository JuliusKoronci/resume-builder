using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISeed
    {
        Task SeedInitData();
        Task SeedSampleData();
    }
}
