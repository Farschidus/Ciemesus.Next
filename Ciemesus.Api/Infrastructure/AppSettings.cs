using Ciemesus.Core.IdentityProvider;
using Ciemesus.Core.Infrastructure;
using System.Collections.Generic;

namespace Ciemesus.Api.Infrastructure
{
    public class AppSettings
    {
        public AppSettings(
            KeyVaultSettings keyVault,
            IdentityProviderSettings identityProviderSettings,
            ConnectionStringSettings connectionStringSettings,
            List<string> allowedCorsOriginsSettings)
        {
            KeyVault = keyVault;
            IdentityProvider = identityProviderSettings;
            ConnectionString = connectionStringSettings;
            AllowedCorsOrigins = allowedCorsOriginsSettings;
        }

        public KeyVaultSettings KeyVault { get; private set; }
        public IdentityProviderSettings IdentityProvider { get; private set; }
        public ConnectionStringSettings ConnectionString { get; private set; }
        public List<string> AllowedCorsOrigins { get; private set; }
    }

    public class KeyVaultSettings
    {
        public string KeyVaultUrl { get; set; }
    }
}
