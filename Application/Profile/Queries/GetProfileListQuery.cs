using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Profile.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Profile.Queries
{
    public class GetProfileListQuery : IRequest<IEnumerable<GetProfileDto>>
    {
    }

    public class GetProfileQueryHandler : IRequestHandler<GetProfileListQuery, IEnumerable<GetProfileDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProfileQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetProfileDto>> Handle(GetProfileListQuery request,
            CancellationToken cancellationToken)
        {
            var profiles = await _dbContext.Profiles.ProjectTo<GetProfileDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return profiles;
        }
    }
}