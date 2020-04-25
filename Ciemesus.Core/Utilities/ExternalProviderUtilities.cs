using System.Collections.Generic;
using System.Linq;

namespace Ciemesus.Core.Utilities
{
    public static class ExternalProviderUtilities
    {
        private static readonly List<KeyValuePair<string, List<string>>> ExternalProviderDomainMaps = new List<KeyValuePair<string, List<string>>>
        {
            new KeyValuePair<string, List<string>>("google", new List<string>
            {
                "google.com",
            }),
        };

        public static bool EmailRequiresExternalProvider(string email)
        {
            if (!EmailUtilities.IsEmail(email))
            {
                return false;
            }

            return ExternalProviderDomainMaps.Any((provider) => provider.Value.Any(y => email.EndsWith($"@{y}")));
        }

        public static string GetExternalProviderByEmail(string email)
            => ExternalProviderDomainMaps.Find((provider) => provider.Value.Any(y => email.EndsWith(y))).Key;
    }
}
