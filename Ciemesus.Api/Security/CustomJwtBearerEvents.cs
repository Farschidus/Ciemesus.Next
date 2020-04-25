using Ciemesus.Core.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ciemesus.Api.Security
{
    public class CustomJwtBearerEvents : JwtBearerEvents
    {
        public const string ApplicationName = "Ciemesus";

        public override async Task TokenValidated(TokenValidatedContext context)
        {
            var mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();
            var httpContextAccessor = context.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();

            var externalUser = context.Principal;
            var identityProviderUserId = externalUser.FindFirstValue("sub");

            var response = await mediator.Send(new ApplicationUserGet.Query
            {
                Application = ApplicationName,
                IdentityProviderUserId = identityProviderUserId,
            });

            if (response.IsValid)
            {
                context.Principal = ClaimsProvider.AssignClaims(context.Principal, response.Result, ApplicationName);
                httpContextAccessor.HttpContext.User = context.Principal;
            }
        }
    }
}
