using System;
using System.Collections.Generic;

namespace Ciemesus.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsAny(this string list, List<string> validContentTypes)
        {
            foreach (string contentType in validContentTypes)
            {
                if (list.Contains(contentType))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
