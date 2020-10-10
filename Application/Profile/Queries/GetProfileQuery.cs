using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Profile.Queries
{
    public class GetProfileQuery : IRequest<Domain.Entities.Profile>
    {
        public int Id;

        public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Domain.Entities.Profile>
        {
            private readonly IApplicationDbContext _dbContext;

            public GetProfileQueryHandler(IApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Domain.Entities.Profile> Handle(GetProfileQuery request,
                CancellationToken cancellationToken)
            {
                var profile = await _dbContext.Profiles.Where(p => p.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                return profile;
            }
        }
    }
}
