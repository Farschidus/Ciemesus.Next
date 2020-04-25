using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ciemesus.Core.Infrastructure
{
    public interface IHttpHandler
    {
        Uri BaseAddress { get; set; }
        AuthenticationHeaderValue Token { get; set; }
        HttpResponseMessage Delete(string url);
        HttpResponseMessage Get(string url);
        HttpResponseMessage Patch(string url, HttpContent content);
        HttpResponseMessage Post(string url, HttpContent content);
        Task<HttpResponseMessage> DeleteAsync(string url);
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PatchAsync(string url, HttpContent content);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
        void Dispose();
    }
}
