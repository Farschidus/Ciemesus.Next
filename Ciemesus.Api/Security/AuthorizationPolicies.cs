using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Ciemesus.Api.Security
{
    public class AuthorizationPolicies
    {
        public static AuthorizationPolicy IsAuthenticated()
        {
            return NewPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole("CiemesusAdministrator")
                .Build();
        }

        private static AuthorizationPolicyBuilder NewPolicyBuilder()
        {
            var scheme = JwtBearerDefaults.AuthenticationScheme;
            return new AuthorizationPolicyBuilder(scheme);
        }
    }
}
