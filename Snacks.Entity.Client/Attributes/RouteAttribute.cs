using System;
using System.Collections.Generic;
using System.Text;

namespace Snacks.Entity.Client.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RouteAttribute : Attribute
    {
        public Uri Uri { get; set; }

        public RouteAttribute(string uri)
        {
            Uri = new Uri(uri);
        }
    }
}
