using System;

namespace Ciemesus.Core.Utilities
{
    public static class Check
    {
        public static class Argument
        {
            public static void IsNotNull(object parameter, string parameterName)
            {
                if (parameter == null)
                {
                    throw new ArgumentNullException(parameterName, $"{parameterName} cannot be null.");
                }
            }

            public static void IsNotNullOrEmpty(string parameter, string parameterName)
            {
                if (string.IsNullOrWhiteSpace(parameter))
                {
                    throw new ArgumentException($"{parameterName} cannot be null or empty.");
                }
            }

            public static void IsNotZeroOrNegative(int parameter, string parameterName)
            {
                if (parameter <= 0)
                {
                    throw new ArgumentOutOfRangeException(parameterName, $"{parameterName} cannot be negative or zero.");
                }
            }

            public static void IsNotNegative(int parameter, string parameterName)
            {
                if (parameter < 0)
                {
                    throw new ArgumentOutOfRangeException(parameterName, $"{parameterName} cannot be negative.");
                }
            }

            public static void IsNotZeroOrNegative(long parameter, string parameterName)
            {
                if (parameter <= 0)
                {
                    throw new ArgumentOutOfRangeException(parameterName, $"{parameterName} cannot be negative or zero.");
                }
            }

            public static void IsNotNegative(long parameter, string parameterName)
            {
                if (parameter < 0)
                {
                    throw new ArgumentOutOfRangeException(parameterName, $"{parameterName} cannot be negative.");
                }
            }

            public static void IsNotZeroOrNegative(float parameter, string parameterName)
            {
                if (parameter <= 0)
                {
                    throw new ArgumentOutOfRangeException(parameterName, $"{parameterName} cannot be negative or zero.");
                }
            }

            public static void IsNotNegative(float parameter, string parameterName)
            {
                if (parameter < 0)
                {
                    throw new ArgumentOutOfRangeException(parameterName, $"{parameterName} cannot be negative.");
                }
            }
        }

        public static class Reference
        {
            public static void IsNotNull(object reference, string referenceName)
            {
                if (reference == null)
                {
                    throw new NullReferenceException($"{referenceName} cannot be null.");
                }
            }

            public static void IsNotEmpty(string reference, string referenceName)
            {
                if (string.IsNullOrEmpty(reference))
                {
                    throw new NullReferenceException($"{referenceName} cannot be null or empty.");
                }
            }
        }
    }
}
