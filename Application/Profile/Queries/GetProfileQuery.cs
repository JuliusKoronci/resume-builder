using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Profile.Queries
{
    public class GetProfileQuery : IRequest<IEnumerable<Domain.Entities.Profile>>
    {
    }

    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, IEnumerable<Domain.Entities.Profile>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetProfileQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Domain.Entities.Profile>> Handle(GetProfileQuery request,
            CancellationToken cancellationToken)
        {
            var profiles = await _dbContext.Profiles.ToListAsync(cancellationToken);

            return profiles?.AsReadOnly();
        }
    }
}
