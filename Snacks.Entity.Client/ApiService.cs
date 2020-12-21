using Newtonsoft.Json;
using Snacks.Entity.Client.Extensions;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Snacks.Entity.Client
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(string baseUri, TimeSpan? timeout = null) : this(new Uri(baseUri), timeout)
        {
        }

        public ApiService(Uri baseUri, TimeSpan? timeout = null)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = baseUri
            };

            if (timeout.HasValue)
            {
                _httpClient.Timeout = timeout.Value;
            }
        }

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResponse> DeleteAsync<TResponse>(string uri)
        {
            return await DeleteAsync<TResponse>(new Uri(uri));
        }

        public async Task<TResponse> DeleteAsync<TResponse>(Uri uri)
        {
            HttpResponseMessage message = await DeleteAsync(uri);

            return await message.ToObjectAsync<TResponse>();
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            return await DeleteAsync(new Uri(uri));
        }

        public async Task<HttpResponseMessage> DeleteAsync(Uri uri)
        {
            HttpResponseMessage message = await _httpClient.DeleteAsync(uri);

            return message;
        }

        public async Task<TResponse> GetAsync<TResponse>(string uri)
        {
            return await GetAsync<TResponse>(new Uri(uri));
        }

        public async Task<TResponse> GetAsync<TResponse>(Uri uri)
        {
            HttpResponseMessage message = await GetAsync(uri);

            return await message.ToObjectAsync<TResponse>();
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            return await GetAsync(new Uri(uri));
        }

        public async Task<HttpResponseMessage> GetAsync(Uri uri)
        {
            HttpResponseMessage message = await _httpClient.GetAsync(uri);

            return message;
        }

        public async Task<TResponse> PostAsync<TResponse>(string uri, object body)
        {
            return await PostAsync<TResponse>(new Uri(uri), body);
        }

        public async Task<TResponse> PostAsync<TResponse>(Uri uri, object body)
        {
            HttpResponseMessage message = await PostAsync(uri, body);

            return await message.ToObjectAsync<TResponse>();
        }

        public async Task<HttpResponseMessage> PostAsync(string uri, object body)
        {
            return await PostAsync(new Uri(uri), body);
        }

        public async Task<HttpResponseMessage> PostAsync(Uri uri, object body)
        {
            HttpResponseMessage message = await _httpClient.PostAsync(uri,
               GetContentFromObject(body));

            return message;
        }

        public async Task<TResponse> PutAsync<TResponse>(string uri, object body)
        {
            return await PutAsync<TResponse>(new Uri(uri), body);
        }

        public async Task<TResponse> PutAsync<TResponse>(Uri uri, object body)
        {
            HttpResponseMessage message = await PutAsync(uri, body);

            return await message.ToObjectAsync<TResponse>();
        }

        public async Task<HttpResponseMessage> PutAsync(string uri, object body)
        {
            return await PutAsync(new Uri(uri), body);
        }

        public async Task<HttpResponseMessage> PutAsync(Uri uri, object body)
        {
            HttpResponseMessage message = await _httpClient.PutAsync(uri,
                GetContentFromObject(body));

            return message;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await _httpClient.SendAsync(request);
        }

        private StringContent GetContentFromObject(object @object)
        {
            return new StringContent(
                JsonConvert.SerializeObject(@object),
                Encoding.UTF8,
                "application/json");
        }
    }
}
