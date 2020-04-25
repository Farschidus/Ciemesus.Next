using System;

namespace Ciemesus.Core.Api.Infrastructure.Pagination
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OrderableAttribute : Attribute
    {
        public OrderableAttribute(string mapsTo)
        {
            MapsTo = mapsTo;
        }

        public string MapsTo { get; set; }
    }
}
