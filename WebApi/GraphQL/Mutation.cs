using System.Threading.Tasks;
using Application.Profile.Commands;
using Application.Profile.Dtos;
using MediatR;

namespace WebApi.GraphQL
{
    public class Mutation
    {
        private readonly IMediator _mediator;

        public Mutation(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> DeleteProfile(int id)
        {
            await _mediator.Send(new DeleteProfileCommand(id));

            return id;
        }

        public async Task<GetProfileDto> CreateProfile(CreateProfileCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<GetProfileDto> UpdateProfile(int id, UpdateProfileDto profile)
        {
            return await _mediator.Send(new UpdateProfileCommand(id, profile));
        }
    }
}
