using Ciemesus.Api.Extensions;
using Ciemesus.Core.Team;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ciemesus.Api.Controllers.Team
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    public class TeamController : Controller
    {
        private readonly IMediator _mediator;

        public TeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam(int id)
        {
            var message = new TeamGet.Query { TeamId = id };

            var response = await _mediator.Send(message);

            return response.HandledResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var message = new ScheduleGet.Query { TeamId = id };

            var response = await _mediator.Send(message);

            return response.HandledResult();
        }
    }
}
