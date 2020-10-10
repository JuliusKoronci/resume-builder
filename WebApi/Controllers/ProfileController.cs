using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Profile.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        public async Task<IEnumerable<Profile>> Get()
        {
            return await Mediator.Send(new GetProfileQuery());
        }
    }
}
