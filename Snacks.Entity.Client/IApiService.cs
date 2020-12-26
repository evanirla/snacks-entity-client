using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Snacks.Entity.Client
{
    public interface IApiService
    {
        void SetHeaders(Action<HttpRequestHeaders> headers);
        Task<TResponse> DeleteAsync<TResponse>(string route);
        Task<HttpResponseMessage> DeleteAsync(string route);
        Task<TResponse> GetAsync<TResponse>(string route);
        Task<HttpResponseMessage> GetAsync(string route);
        Task<TResponse> PatchAsync<TResponse>(string route, object body);
        Task<HttpResponseMessage> PatchAsync(string route, object body);
        Task<TResponse> PostAsync<TResponse>(string route, object body);
        Task<HttpResponseMessage> PostAsync(string route, object body);
        Task<TResponse> PutAsync<TResponse>(string route, object body);
        Task<HttpResponseMessage> PutAsync(string route, object body);
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
