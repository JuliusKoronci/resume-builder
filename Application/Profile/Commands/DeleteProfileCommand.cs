using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;

namespace Application.Profile.Commands
{
    public class DeleteProfileCommand : IRequest<Unit?>
    {
        public DeleteProfileCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    public class HandleDeleteProfile : IRequestHandler<DeleteProfileCommand, Unit?>
    {
        private readonly IApplicationDbContext _dbContext;

        public HandleDeleteProfile(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit?> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Profiles.FindAsync(request.Id);

            if (entity == null) return null;

            _dbContext.Profiles.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
