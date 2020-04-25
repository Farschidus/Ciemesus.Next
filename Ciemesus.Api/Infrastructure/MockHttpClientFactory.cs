using System.Net.Http;

namespace Ciemesus.Api.Infrastructure
{
    public class MockHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
