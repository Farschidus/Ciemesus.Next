using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ciemesus.Core.Infrastructure
{
    public class HttpClientHandler : IHttpHandler, IDisposable
    {
        private readonly HttpClient _client;
        private bool _disposed;
        private AuthenticationHeaderValue _token;
        private Uri _baseAddress;

        public HttpClientHandler(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient();
        }

        public AuthenticationHeaderValue Token
        {
            get
            {
                return _token;
            }
            set
            {
                _token = value;
                _client.DefaultRequestHeaders.Authorization = value;
            }
        }

        public Uri BaseAddress
        {
            get
            {
                return _baseAddress;
            }
            set
            {
                if (!value.ToString().EndsWith("/"))
                {
                    _baseAddress = new Uri($"{value.ToString()}/");
                }
                else
                {
                    _baseAddress = new Uri(value.ToString());
                }
            }
        }

        public HttpResponseMessage Delete(string url)
        {
            return DeleteAsync(url).Result;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await _client.DeleteAsync($"{BaseAddress}{TransformUrl(url)}");
        }

        public HttpResponseMessage Get(string url)
        {
            return GetAsync(url).Result;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync($"{BaseAddress}{TransformUrl(url)}");
        }

        public HttpResponseMessage Patch(string url, HttpContent content)
        {
            return PatchAsync(url, content).Result;
        }

        public async Task<HttpResponseMessage> PatchAsync(string url, HttpContent content)
        {
            var method = new HttpMethod("PATCH");
            using var request = new HttpRequestMessage(method, $"{BaseAddress}{TransformUrl(url)}")
            {
                Content = content,
            };

            return await _client.SendAsync(request);
        }

        public HttpResponseMessage Post(string url, HttpContent content)
        {
            return PostAsync(url, content).Result;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return await _client.PostAsync($"{BaseAddress}{TransformUrl(url)}", content);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _client.Dispose();
            }

            _disposed = true;
        }

        private string TransformUrl(string url = "")
        {
            return url.TrimStart('/');
        }
    }
}
