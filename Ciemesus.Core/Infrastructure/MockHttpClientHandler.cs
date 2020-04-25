using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ciemesus.Core.Infrastructure
{
    public class MockHttpClientHandler : IHttpHandler
    {
        public string ExpectedResponse { get; set; } = "Success!";
        public HttpStatusCode ExpectedStatusCode { get; set; } = (HttpStatusCode)200;

        public string LastUrl { get; set; }
        public HttpContent LastContent { get; set; }
        public string LastMethod { get; set; }

        public Uri BaseAddress { get; set; }
        public AuthenticationHeaderValue Token { get; set; }

        public HttpResponseMessage Delete(string url)
        {
            using var response = DeleteAsync(url);
            return response.Result;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            LastUrl = url;
            LastContent = null;
            LastMethod = "DELETE";

            await Task.Run(() => { });

            return new HttpResponseMessage(ExpectedStatusCode);
        }

        public HttpResponseMessage Get(string url)
        {
            using var response = GetAsync(url);
            return response.Result;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            LastUrl = url;
            LastContent = null;
            LastMethod = "GET";

            var response = new HttpResponseMessage(ExpectedStatusCode)
            {
                Content = new StringContent(ExpectedResponse),
            };

            await Task.Run(() => { });

            return response;
        }

        public HttpResponseMessage Patch(string url, HttpContent content)
        {
            using var response = PostAsync(url, content);
            return response.Result;
        }

        public async Task<HttpResponseMessage> PatchAsync(string url, HttpContent content)
        {
            LastUrl = url;
            LastContent = content;
            LastMethod = "PATCH";

            await Task.Run(() => { });

            return new HttpResponseMessage(ExpectedStatusCode);
        }

        public HttpResponseMessage Post(string url, HttpContent content)
        {
            using var response = PostAsync(url, content);
            return response.Result;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            LastUrl = url;
            LastContent = content;
            LastMethod = "POST";

            var response = new HttpResponseMessage(ExpectedStatusCode)
            {
                Content = new StringContent(ExpectedResponse),
            };

            await Task.Run(() => { });

            return response;
        }

        public void Dispose()
        {
        }
    }
}
