using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Snacks.Entity.Client
{
    public interface IApiService
    {
        Task<TResponse> DeleteAsync<TResponse>(string uri);
        Task<TResponse> DeleteAsync<TResponse>(Uri uri);
        Task<HttpResponseMessage> DeleteAsync(string uri);
        Task<HttpResponseMessage> DeleteAsync(Uri uri);
        Task<TResponse> GetAsync<TResponse>(string uri);
        Task<TResponse> GetAsync<TResponse>(Uri uri);
        Task<HttpResponseMessage> GetAsync(string uri);
        Task<HttpResponseMessage> GetAsync(Uri uri);
        Task<TResponse> PostAsync<TResponse>(string uri, object body);
        Task<TResponse> PostAsync<TResponse>(Uri uri, object body);
        Task<HttpResponseMessage> PostAsync(string uri, object body);
        Task<HttpResponseMessage> PostAsync(Uri uri, object body);
        Task<TResponse> PutAsync<TResponse>(string uri, object body);
        Task<TResponse> PutAsync<TResponse>(Uri uri, object body);
        Task<HttpResponseMessage> PutAsync(string uri, object body);
        Task<HttpResponseMessage> PutAsync(Uri uri, object body);
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
