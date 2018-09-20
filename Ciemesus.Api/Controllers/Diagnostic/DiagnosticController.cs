using Microsoft.AspNetCore.Mvc;
using System;

namespace Ciemesus.Api.Controllers.Diagnostic
{
    [Route("api/[controller]/[action]")]
    public class DiagnosticController : Controller
    {
        [HttpGet]
        public ActionResult Ping()
        {
            return Ok("Pong");
        }

        [HttpGet]
        public ActionResult Throw()
        {
            throw new Exception("test");
        }
    }
}
