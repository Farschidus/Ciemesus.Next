using Ciemesus.Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ciemesus.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCors();
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<CiemesusIdentityDb>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
