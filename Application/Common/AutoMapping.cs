using Application.Profile.Dtos;

namespace Application.Common
{
    public class AutoMapping : AutoMapper.Profile
    {
        public AutoMapping()
        {
            CreateMap<Domain.Entities.Profile, GetProfileDto>();
            CreateMap<UpdateProfileDto, Domain.Entities.Profile>();
        }
    }
}
