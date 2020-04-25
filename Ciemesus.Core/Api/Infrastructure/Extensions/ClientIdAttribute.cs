using System;

namespace Ciemesus.Core.Api.Infrastructure.Extensions
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ClientIdAttribute : Attribute
    {
        public ClientIdAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
