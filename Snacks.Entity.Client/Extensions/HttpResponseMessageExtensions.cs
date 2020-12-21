using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Snacks.Entity.Client.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        private static readonly JsonSerializer _jsonSerializer = new JsonSerializer();

        public static async Task<T> ToObjectAsync<T>(this HttpResponseMessage message)
        {
            using (Stream stream = await message.Content.ReadAsStreamAsync())
            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonReader jsonReader = new JsonTextReader(streamReader))
            {
                return _jsonSerializer.Deserialize<T>(jsonReader);
            }
        }
    }
}
