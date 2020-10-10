using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Profile.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProfileController : ApiBaseController
    {
        [HttpGet]
        public async Task<IEnumerable<Profile>> Get()
        {
            var profileList = await Mediator.Send(new GetProfileListQuery());

            return profileList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetById(int id)
        {
            var profile = await Mediator.Send(new GetProfileQuery {Id = id});

            if (null == profile) return NotFound();

            return profile;
        }
    }
}
