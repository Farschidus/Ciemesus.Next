using System;

namespace Ciemesus.Core.Api.Infrastructure.Pagination
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterableAttribute : Attribute
    {
        public FilterableAttribute(string mapsTo)
        {
            MapsTo = mapsTo;
        }

        public string MapsTo { get; set; }
    }
}
