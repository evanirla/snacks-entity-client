using System;
using System.Collections.Generic;
using System.Text;

namespace Snacks.Entity.Client.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RouteAttribute : Attribute
    {
        public string Route { get; set; }

        public RouteAttribute(string route)
        {
            Route = route;
        }
    }
}
