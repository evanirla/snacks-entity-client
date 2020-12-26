using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Snacks.Entity.Client
{
    public class ApiOptions
    {
        public Uri BaseAddress { get; set; }
        public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromSeconds(30);
        public HttpClient HttpClient { get; set; }
    }
}
