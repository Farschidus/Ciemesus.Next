using Ciemesus.Core.Utilities;
using Microsoft.Extensions.Configuration;

namespace Ciemesus.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static TConfig Bind<TConfig>(this IConfiguration configuration, string key)
        where TConfig : class, new()
        {
            Check.Argument.IsNotNull(configuration, nameof(configuration));

            var config = new TConfig();
            configuration.Bind(key, config);
            return config;
        }
    }
}
