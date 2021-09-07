using Autofac;
using Chatter.Infrastructure.CQRS.Commands;
using Chatter.Infrastructure.CQRS.Dispatchers;
using Chatter.Infrastructure.CQRS.Queries;
using System.Reflection;

namespace Chatter.Infrastructure.IoC.Modules
{
    public class CqrsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CqrsModule)
                           .GetTypeInfo()
                           .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
               .AsClosedTypesOf(typeof(IQueryHandler<,>))
               .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerLifetimeScope();
            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>().InstancePerLifetimeScope();
           
            builder.RegisterType<Dispatcher>().As<IDispatcher>().InstancePerLifetimeScope();
        }
    }
}
