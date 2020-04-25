using System;
using System.Security.Claims;
using Ciemesus.Core.Authentication;
using Microsoft.AspNetCore.Http;

namespace Ciemesus.Api.Security
{
    public class PrincipalProvider
    {
        public const string ApplicationName = "Ciemesus";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PrincipalProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public CiemesusPrincipal Current()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var defaultPrincipal = _httpContextAccessor.HttpContext.User;
                if (defaultPrincipal != null)
                {
                    var principal = MapToCiemesusPrincipal(defaultPrincipal);
                    return principal;
                }
                else
                {
                    return UnauthorizedPrincipal();
                }
            }
            else
            {
                return UnauthorizedPrincipal();
            }
        }

        private static CiemesusPrincipal MapToCiemesusPrincipal(ClaimsPrincipal defaultPrincipal)
        {
            var principal = new CiemesusPrincipal(defaultPrincipal)
            {
                UserId = Convert.ToInt32(defaultPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)),
                Application = ApplicationName,
            };

            return principal;
        }

        private static CiemesusPrincipal UnauthorizedPrincipal()
        {
            var defaultPrincipal = new ClaimsPrincipal();
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(identity.NameClaimType, "Unauthorized User"));

            defaultPrincipal.AddIdentity(identity);

            var principal = new CiemesusPrincipal(defaultPrincipal);

            return principal;
        }
    }
}
