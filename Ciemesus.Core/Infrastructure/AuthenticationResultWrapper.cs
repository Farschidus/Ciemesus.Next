using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Ciemesus.Core.Infrastructure
{
    public class AuthenticationResultWrapper : IAuthenticationResultWrapper
    {
        private bool _disposed;
        public AuthenticationResult Result { get; set; }

        public string AccessToken => Result.AccessToken;

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
        }
    }
}
