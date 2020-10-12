using Application.Profile.Queries;

namespace Application.Common
{
    public class AutoMapping : AutoMapper.Profile
    {
        public AutoMapping()
        {
            CreateMap<Domain.Entities.Profile, GetProfileDto>();
        }
    }
}
