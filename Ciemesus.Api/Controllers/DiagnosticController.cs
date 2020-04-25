using Ciemesus.Core.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ciemesus.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DiagnosticController : Controller
    {
        private readonly CiemesusDb _db;

        public DiagnosticController(CiemesusDb db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult Ping()
        {
            return Ok("Pong");
        }

        [HttpGet]
        [Route("401")]
        public ActionResult GetUnauthorized()
        {
            return Unauthorized();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Anonymous()
        {
            if (_db.Database.CanConnect())
            {
                return Ok("Connected to database");
            }

            return StatusCode(500, "Cannot connect to database");
        }

        [HttpGet, AllowAnonymous]
        [Route("400")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet, AllowAnonymous]
        [Route("403")]
        public ActionResult GetForbid()
        {
            return Forbid();
        }
    }
}
