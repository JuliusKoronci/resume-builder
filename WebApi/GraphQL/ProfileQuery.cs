using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Profile.Dtos;
using Application.Profile.Queries;
using MediatR;

namespace WebApi.GraphQL
{
    public class ProfileQuery
    {
        private readonly IMediator _mediator;

        public ProfileQuery(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<GetProfileDto>> GetProfileList()
        {
            return await _mediator.Send(new GetProfileListQuery());
        }

        public async Task<GetProfileDto> GetProfile(int id)
        {
            return await _mediator.Send(new GetProfileQuery {Id = id});
        }
    }
}
