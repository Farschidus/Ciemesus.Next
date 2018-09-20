using Ciemesus.Core;
using Ciemesus.Core.Data;
using FluentValidation;
using MediatR;
using StructureMap;
using StructureMap.Pipeline;

namespace Ciemesus.Api.Infrastructure
{
    public class ApiRegistry : Registry
    {
        public ApiRegistry(string dbConnectionString)
        {
            Scan(scanner =>
            {
                scanner.AssembliesAndExecutablesFromApplicationBaseDirectory(assembly => assembly.FullName.StartsWith("Ciemesus"));
                scanner.WithDefaultConventions();
                scanner.AddAllTypesOf(typeof(ICiemesusRequest<>));
                scanner.AddAllTypesOf(typeof(ICiemesusRequestHandler<,>));
                scanner.AddAllTypesOf(typeof(ICiemesusAsyncRequestHandler<,>));
                scanner.AddAllTypesOf(typeof(ICiemesusResponse));
                scanner.AddAllTypesOf(typeof(ICiemesusResponse<>));
                scanner.AddAllTypesOf(typeof(IValidator<>));
            });

            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
            For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
            For<IMediator>().Use<Mediator>();
            For<CiemesusDb>().Use(() => new CiemesusDb(dbConnectionString)).LifecycleIs<TransientLifecycle>();
            For(typeof(IPipelineBehavior<,>)).Add(typeof(ValidationPipeline<,>));
        }
    }
}
