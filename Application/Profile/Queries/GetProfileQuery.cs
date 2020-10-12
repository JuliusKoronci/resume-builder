using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Profile.Queries
{
    public class GetProfileQuery : IRequest<GetProfileDto>
    {
        public int Id;

        public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, GetProfileDto>
        {
            private readonly IApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetProfileQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<GetProfileDto> Handle(GetProfileQuery request,
                CancellationToken cancellationToken)
            {
                var profile = await _dbContext.Profiles.Where(p => p.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                return _mapper.Map<GetProfileDto>(profile);
            }
        }
    }
}
