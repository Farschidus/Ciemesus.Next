using Ciemesus.Api.Security;
using Ciemesus.Core;
using Ciemesus.Core.Authentication;
using Ciemesus.Core.Contracts;
using Ciemesus.Core.Data;
using Ciemesus.Core.IdentityProvider;
using Ciemesus.Core.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StructureMap;
using StructureMap.Pipeline;

namespace Ciemesus.Api.Infrastructure
{
    public class ApiRegistry : Registry
    {
        public ApiRegistry(AppSettings settings)
        {
            Scan(scanner =>
            {
                scanner.AssembliesAndExecutablesFromApplicationBaseDirectory(assembly => assembly.FullName.StartsWith("Ciemesus"));
                scanner.WithDefaultConventions();
                scanner.AddAllTypesOf(typeof(IResponseBase<>));
                scanner.AddAllTypesOf(typeof(IValidator<>));
                scanner.AddAllTypesOf(typeof(AbstractHandler<,>));
                scanner.AddAllTypesOf(typeof(AbstractHandler<>));
                scanner.AddAllTypesOf(typeof(IValidator<>));
            });

            For<ServiceFactory>().Use<ServiceFactory>(ctx => ctx.GetInstance);

            // mediator and its pipeline
            For<IMediator>().Use<Mediator>();
            For(typeof(IPipelineBehavior<,>)).Add(typeof(ValidationPipeline<,>));

            // Application Settings
            For<KeyVaultSettings>().Use(settings.KeyVault).LifecycleIs<SingletonLifecycle>();
            For<ConnectionStringSettings>().Use(settings.ConnectionString).LifecycleIs<SingletonLifecycle>();
            For<IdentityProviderSettings>().Use(settings.IdentityProvider).LifecycleIs<SingletonLifecycle>();

            // database context
            For<CiemesusDb>().Use<CiemesusDb>().LifecycleIs<TransientLifecycle>();

            // principal provider
            For<IHttpContextAccessor>().Use<HttpContextAccessor>().LifecycleIs<SingletonLifecycle>();
            For<CiemesusPrincipal>().Use(ctx => ctx.GetInstance<PrincipalProvider>().Current()).LifecycleIs<TransientLifecycle>();

            // HttpClient used for IoTManagement integration
            For<IHttpHandler>().Use<HttpClientHandler>();

            // localization in Ciemesus.Core
            For<ILoggerFactory>().Use<LoggerFactory>();
            For(typeof(IOptions<>)).Add(typeof(OptionsManager<>)).LifecycleIs<SingletonLifecycle>();
            For(typeof(IOptionsFactory<>)).Add(typeof(OptionsFactory<>)).LifecycleIs<TransientLifecycle>();
            For<IStringLocalizerFactory>().Use<ResourceManagerStringLocalizerFactory>().LifecycleIs<SingletonLifecycle>();
            For(typeof(IStringLocalizer<>)).Add(typeof(StringLocalizer<>)).LifecycleIs<TransientLifecycle>();
        }
    }
}
