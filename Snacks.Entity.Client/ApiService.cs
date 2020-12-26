using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Snacks.Entity.Client.Extensions;
using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Snacks.Entity.Client
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(IOptions<ApiOptions> options)
        {
            ApiOptions apiOptions = options.Value;

            if (apiOptions.HttpClient != null)
            {
                _httpClient = apiOptions.HttpClient;
            }
            else
            {
                _httpClient = new HttpClient();
            }

            if (apiOptions.BaseAddress != null)
            {
                _httpClient.BaseAddress = apiOptions.BaseAddress;
            }

            _httpClient.Timeout = apiOptions.RequestTimeout;
        }

        public void SetHeaders(Action<HttpRequestHeaders> headers)
        {
            headers.Invoke(_httpClient.DefaultRequestHeaders);
        }

        public async Task<TResponse> DeleteAsync<TResponse>(string route)
        {
            HttpResponseMessage message = await DeleteAsync(route);

            message.EnsureSuccessStatusCode();

            return await message.ToObjectAsync<TResponse>();
        }

        public async Task<HttpResponseMessage> DeleteAsync(string route)
        {
            HttpResponseMessage message = await _httpClient.DeleteAsync(route);

            return message;
        }

        public async Task<TResponse> GetAsync<TResponse>(string route)
        {
            HttpResponseMessage message = await GetAsync(route);

            message.EnsureSuccessStatusCode();

            return await message.ToObjectAsync<TResponse>();
        }

        public async Task<HttpResponseMessage> GetAsync(string route)
        {
            HttpResponseMessage message = await _httpClient.GetAsync(route);

            return message;
        }

        public async Task<TResponse> PatchAsync<TResponse>(string route, object body)
        {
            HttpResponseMessage message = await PatchAsync(route, body);

            return await message.ToObjectAsync<TResponse>();
        }

        public async Task<HttpResponseMessage> PatchAsync(string route, object body)
        {
            HttpResponseMessage message = await _httpClient.PatchAsync(route, GetJSONFromObject(body));

            return message;
        }

        public async Task<TResponse> PostAsync<TResponse>(string route, object body)
        {
            HttpResponseMessage message = await PostAsync(route, body);

            message.EnsureSuccessStatusCode();

            return await message.ToObjectAsync<TResponse>();
        }

        public async Task<HttpResponseMessage> PostAsync(string route, object body)
        {
            HttpResponseMessage message = await _httpClient.PostAsync(route,
               GetJSONFromObject(body));

            return message;
        }

        public async Task<TResponse> PutAsync<TResponse>(string route, object body)
        {
            HttpResponseMessage message = await PutAsync(route, body);

            message.EnsureSuccessStatusCode();

            return await message.ToObjectAsync<TResponse>();
        }

        public async Task<HttpResponseMessage> PutAsync(string route, object body)
        {
            HttpResponseMessage message = await _httpClient.PutAsync(route,
                GetJSONFromObject(body));

            return message;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await _httpClient.SendAsync(request);
        }

        private StringContent GetJSONFromObject(object @object)
        {
            return new StringContent(
                JsonConvert.SerializeObject(@object),
                Encoding.UTF8,
                "application/json");
        }
    }
}
