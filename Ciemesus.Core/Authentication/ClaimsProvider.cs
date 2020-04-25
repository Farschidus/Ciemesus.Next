using System.Linq;
using System.Security.Claims;

namespace Ciemesus.Core.Authentication
{
    public static class ClaimsProvider
    {
        public static ClaimsPrincipal AssignClaims(ClaimsPrincipal principal, string application)
        {
            var identity = principal.Identity as ClaimsIdentity;
            identity.AddClaim(new Claim("Application", application));

            return principal;
        }

        public static ClaimsPrincipal AssignClaims(ClaimsPrincipal principal, ApplicationUserGet.QueryResult user, string application)
        {
            var identity = principal.Identity as ClaimsIdentity;

            identity.AddClaim(new Claim("Application", application));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim(identity.NameClaimType, user.Name));

            if (!string.IsNullOrEmpty(user.Email))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            }
            else
            {
                identity.AddClaim(new Claim("UserName", user.UserName));
            }

            user.ApplicationRoles.ToList().ForEach(x =>
            {
                // for role-based authorization policy
                identity.AddClaim(new Claim(identity.RoleClaimType, x));

                // default role claim type
                identity.AddClaim(new Claim(ClaimTypes.Role, x));
            });

            user.SiteIds.ToList().ForEach(x =>
            {
                identity.AddClaim(new Claim("Site", x.ToString()));
            });

            return principal;
        }
    }
}
