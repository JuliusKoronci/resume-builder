using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Profile.Commands;
using Application.Profile.Dtos;
using Application.Profile.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProfileController : ApiBaseController
    {
        [HttpGet]
        public async Task<IEnumerable<GetProfileDto>> Get()
        {
            var profileList = await Mediator.Send(new GetProfileListQuery());

            return profileList;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var profile = await Mediator.Send(new GetProfileQuery {Id = id});

            if (profile == null) return NotFound();

            return Ok(profile);
        }

        [HttpPost]
        public async Task<ActionResult<GetProfileDto>> Create(CreateProfileCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetProfileDto>> Update([FromRoute] int id, [FromBody] UpdateProfileDto profile)
        {
            var command = new UpdateProfileCommand(id, profile);
            var result = await Mediator.Send(command);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var deleted = await Mediator.Send(new DeleteProfileCommand(id));

            if (deleted == null) return NotFound();
            return NoContent();
        }
    }
}
