using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Profile.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Profile.Commands
{
    public class UpdateProfileCommand : IRequest<GetProfileDto>
    {
        public UpdateProfileDto ProfileDto;

        public UpdateProfileCommand(int id, UpdateProfileDto profile)
        {
            Id = id;
            ProfileDto = profile;
        }

        public int Id { get; set; }
    }

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, GetProfileDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateProfileCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GetProfileDto> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Profiles.FindAsync(request.Id);

            if (entity == null) return null;

            _mapper.Map(request.ProfileDto, entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GetProfileDto>(entity);
        }
    }
}
