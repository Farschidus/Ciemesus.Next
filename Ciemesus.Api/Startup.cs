using Ciemesus.Api.Extensions;
using Ciemesus.Api.Infrastructure;
using Ciemesus.Api.Security;
using Ciemesus.Core.IdentityProvider;
using Ciemesus.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StructureMap;
using System;
using System.Collections.Generic;

namespace Ciemesus.Api
{
    public class Startup
    {
        private const string CiemesusApiAllowSpecificOrigins = "_ciemesusApiAllowSpecificOrigins";

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        private static Container Container { get; set; }

        public static IServiceProvider ConfigureIoC(IServiceCollection services, AppSettings settings)
        {
            Container = new Container();

            Container.Configure(config =>
            {
                config.AddRegistry(new ApiRegistry(settings));
                config.Populate(services);
            });

            Container.AssertConfigurationIsValid();

            return Container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // After you're confident in the sustainability of the HTTPS configuration, increase the HSTS max-age value; a commonly used value is one year.
                // See ConfigureServices method below
                app.UseHsts();
            }

            app.UseRouting();

            app.UseCors(CiemesusApiAllowSpecificOrigins);

            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Request.Path.Value == "/")
                {
                    ctx.Response.StatusCode = 200;
                }
            });

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var settings = CreateSettings();

            services.AddCors(options =>
            {
                options.AddPolicy(CiemesusApiAllowSpecificOrigins, builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetPreflightMaxAge(TimeSpan.FromSeconds(7200));

                    if (Environment.IsDevelopment())
                    {
                        builder.AllowAnyOrigin();
                    }
                    else
                    {
                        builder.WithOrigins(settings.AllowedCorsOrigins.ToArray());
                    }
                });
            });

            services.AddMvc(options =>
                {
                    // Add authorize filter to authorize users who have access to application
                    options.Filters.Add(new AuthorizeFilter(AuthorizationPolicies.IsAuthenticated()));
                    options.Filters.Add(new ProducesAttribute("application/json"));

                    // Add model binder for int array input parameters
                    options.ModelBinderProviders.Insert(0, new IntArrayModelBinderProvider());
                })
                .AddNewtonsoftJson()
                .AddControllersAsServices();

            services.AddSecurity(settings.IdentityProvider);

            services.AddHttpClient();

            if (!Environment.IsDevelopment())
            {
                services.AddHsts(options =>
                {
                    options.Preload = true;
                    options.IncludeSubDomains = true;
                    options.MaxAge = TimeSpan.FromDays(365);
                });

                services.AddApplicationInsightsTelemetry(Configuration);
            }

            return ConfigureIoC(services, settings);
        }

        public AppSettings CreateSettings()
        {
            var keyVault = Configuration.Bind<KeyVaultSettings>("KeyVault");
            var identityProviderSettings = Configuration.Bind<IdentityProviderSettings>("CiemesusApiIdentityProvider");
            var connectionStrings = Configuration.Bind<ConnectionStringSettings>("ConnectionStrings");
            var allowedCorsOriginsSettings = Configuration.Bind<List<string>>("AllowedCorsOrigins");

            var settings = new AppSettings(
                keyVault,
                identityProviderSettings,
                connectionStrings,
                allowedCorsOriginsSettings);

            return settings;
        }
    }
}
