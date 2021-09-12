using Autofac;
using Chatter.Core.Entities;
using Chatter.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Reflection;

namespace Chatter.Infrastructure.IoC.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServiceModule)
                            .GetTypeInfo()
                            .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();


            builder.RegisterType<PasswordHasher<User>>()
                        .As<IPasswordHasher<User>>();


            builder.RegisterType<JwtHandler>()
                     .As<IJwtHandler>()
                     .SingleInstance();
        }
    }
}
