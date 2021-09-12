using Autofac;
using Chatter.Infrastructure.Extensions;
using Chatter.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace Chatter.Infrastructure.IoC.Modules
{
    public class SettingsModule : Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<JwtSettings>("Jwt"))
                .SingleInstance();
        }
    }
}
