using Microsoft.Extensions.Configuration;

namespace Chatter.Infrastructure.Extensions
{
    public static class SettingsExtensions
    {
        public static TModel GetSettings<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);
            return model;
        }
    }
}
