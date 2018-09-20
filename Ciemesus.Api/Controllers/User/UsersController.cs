using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ciemesus.Api.Extensions;
using Ciemesus.Core.User.Identity;
using Microsoft.AspNetCore.Authorization;
using Ciemesus.Core.User;

namespace Ciemesus.Api.Controllers.Admin.User
{
    [Authorize]
    [Route("api/admin/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreate.Command message)
        {
            var response = await _mediator.Send(message);

            return response.HandledResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] UserEdit.Command message)
        {
            var response = await _mediator.Send(message);

            return response.HandledResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _mediator.Send(new UserDelete.Command { Id = id });

            return response.HandledResult();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var message = new UserGet.Query();
            var response = await _mediator.Send(message);

            return response.HandledResult();
        }
    }
}
