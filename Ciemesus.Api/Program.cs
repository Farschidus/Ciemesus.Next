using Ciemesus.Core.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Ciemesus.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(true)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    if (context.HostingEnvironment.EnvironmentName.ToLower() != "development")
                    {
                        var config = builder.Build();
                        var keyVaultUrl = config["KeyVault:BaseUrl"];

                        builder.AddKeyVaultConfigurationProvider(keyVaultUrl);
                    }
                })
                .UseStartup<Startup>()
                .Build();
    }
}
