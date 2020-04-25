using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System.Threading.Tasks;

namespace Ciemesus.Core.Utilities
{
    public class Azure
    {
        public static string GetSecretValue(string keyVaultUrl, string secretKey)
        {
            var tokenProvider = new AzureServiceTokenProvider();

            using var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback));

            var secret = Task.Run(() => keyVaultClient.GetSecretAsync(keyVaultUrl, secretKey));
            Task.WaitAll(secret);

            return secret.Result.Value;
        }
    }
}
