using Ciemesus.Authentication.Extensions;
using Ciemesus.Authentication.Models;
using Ciemesus.Core.Authentication.Identity;
using IdentityServer4.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ciemesus.Authentication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IIdentityServerInteractionService _interaction;

        public AccountController(IMediator mediator, IIdentityServerInteractionService interaction)
        {
            _mediator = mediator;
            _interaction = interaction;
        }

        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            var response = await _mediator.Send(new UserLogin.Command
            {
                Email = model.Email,
                Password = model.Password,
                RememberLogin = model.RememberLogin
            });

            if (response.IsValid)
            {
                return RedirectToLocal(model.ReturnUrl);
            }
            else
            {
                ModelState.AddErrors(response);
            }

            return View();
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await Logout(new LogoutModel {  LogoutId = logoutId });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutModel logoutModel)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutModel.LogoutId);
            await HttpContext.Authentication.SignOutAsync();

            if (string.IsNullOrEmpty(logout?.PostLogoutRedirectUri) == false)
            {
                Response.Redirect(logout.PostLogoutRedirectUri);
            }

            return View();
        }
        
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public class LogoutModel
        {
            public string LogoutId { get; set; }
        }
    }
}
