using Ciemesus.Api.Extensions;
using Ciemesus.Core.Comment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ciemesus.Api.Controllers.Comment
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class CommentController : Controller
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentList(int id)
        {
            var response = await _mediator.Send(new CommentGet.Query { CiemesusId = id });

            return response.HandledResult();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentCreate.Command message)
        {
            var response = await _mediator.Send(message);

            return response.HandledResult();
        }

    }
}
