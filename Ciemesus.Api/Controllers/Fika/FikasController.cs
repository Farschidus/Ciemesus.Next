using Ciemesus.Api.Extensions;
using Ciemesus.Core.Ciemesus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;

namespace Ciemesus.Api.Controllers.Ciemesus
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    public class CiemesusController : Controller
    {
        private readonly IMediator _mediator;
        private readonly PhysicalFileProvider _fileProvider;

        public CiemesusController(IMediator mediator, IFileProvider fileProvider)
        {
            _mediator = mediator;
            _fileProvider = fileProvider as PhysicalFileProvider;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCiemesusList(int id)
        {
            var response = await _mediator.Send(new CiemesusList.Query { TeamId = id });

            return response.HandledResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCiemesus(int id)
        {
            var message = new CiemesusGet.Query { CiemesusId = id };

            var response = await _mediator.Send(message);

            return response.HandledResult();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int fikaId, [FromBody] CiemesusCreate.Command message)
        {
            if (fikaId != message.CiemesusId)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(message);

            return response.HandledResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int fikaId, int id, [FromBody] CiemesusEdit.Command message)
        {
            if (fikaId != message.CiemesusId || id != message.CiemesusId)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(message);

            return response.HandledResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var message = new CiemesusDelete.Command { CiemesusId = id };

            var response = await _mediator.Send(message);

            return response.HandledResult();
        }

        [HttpPost]
        public async Task<IActionResult> PostImage([FromForm] CiemesusImageGet.Command message)
        {
            message.FilePath = _fileProvider.Root;
            var response = await _mediator.Send(message);

            return Ok();
        }
    }
}
