using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Profile.Queries;
using AutoMapper;
using MediatR;

namespace Application.Profile.Commands
{
    public class CreateProfileCommand : IRequest<GetProfileDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string WebSite { get; set; }
    }

    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, GetProfileDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateProfileCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GetProfileDto> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Profile
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                WebSite = request.WebSite
            };

            await _dbContext.Profiles.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GetProfileDto>(entity);
        }
    }
}
