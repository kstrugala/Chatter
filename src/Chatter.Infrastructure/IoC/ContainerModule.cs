using Autofac;
using Chatter.Infrastructure.IoC.Modules;
using Chatter.Infrastructure.Mapper;
using Microsoft.Extensions.Configuration;

namespace Chatter.Infrastructure.IoC
{
    public class ContainerModule : Module
    {
        private readonly IConfiguration _configuration;
        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize()).SingleInstance();

            builder.RegisterModule(new SettingsModule(_configuration));
         
            builder.RegisterModule<CqrsModule>();
            builder.RegisterModule<ServiceModule>();
        }
    }
}
