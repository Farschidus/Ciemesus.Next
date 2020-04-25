using Ciemesus.Api.Security;
using Ciemesus.Core.IdentityProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ciemesus.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IdentityProviderSettings settings)
        {
            services.AddScoped<CustomJwtBearerEvents>()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = settings.Authority;
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "Ciemesus.Api";
                    options.JwtBearerEvents = services.BuildServiceProvider().GetService<CustomJwtBearerEvents>();
                });

            return services;
        }
    }
}
