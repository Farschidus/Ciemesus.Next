using System;
using System.Reflection;

namespace Ciemesus.Core.Api.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string GetClientId(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            ClientIdAttribute[] attributes = (ClientIdAttribute[])fieldInfo.GetCustomAttributes(
                typeof(ClientIdAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Name;
            }

            return value.ToString();
        }
    }
}
