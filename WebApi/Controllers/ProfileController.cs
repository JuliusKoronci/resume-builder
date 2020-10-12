using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Profile.Commands;
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
    }
}
