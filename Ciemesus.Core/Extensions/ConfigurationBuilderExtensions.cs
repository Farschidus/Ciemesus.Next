using Ciemesus.Core.Utilities;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace Ciemesus.Core.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        private static KeyVaultClient _keyVaultClient;

        public static void AddKeyVaultConfigurationProvider(this IConfigurationBuilder builder, string keyVaultUrl)
        {
            Check.Reference.IsNotEmpty(keyVaultUrl, nameof(keyVaultUrl));

            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            _keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

            builder.AddAzureKeyVault(keyVaultUrl, _keyVaultClient, new DefaultKeyVaultSecretManager());
        }
    }
}
