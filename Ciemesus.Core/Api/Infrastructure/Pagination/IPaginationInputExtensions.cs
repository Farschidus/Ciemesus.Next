using System;
using System.Linq;
using System.Linq.Expressions;

namespace Ciemesus.Core.Api.Infrastructure.Pagination
{
    public static class IPaginationInputExtensions
    {
        public static readonly string DateTimeFormatPattern = "yyyy-MM-dd HH:mm:ss.fffffff";

        public static readonly string DateTimeOffsetFormatPattern = "yyyy-MM-dd HH:mm:ss.fffffff zzz";

        public static Func<TModel, object> GetLambda<TModel>(this IPaginationInput<TModel> request)
        {
            var objectToString = typeof(object).GetMethod("ToString");
            var dateTimeToString = typeof(DateTime).GetMethod("ToString", new Type[] { typeof(string) });
            var dateTimeOffsetToString = typeof(DateTimeOffset).GetMethod("ToString", new Type[] { typeof(string) });
            var dateTimeFormat = Expression.Constant(DateTimeFormatPattern);
            var dateTimeOffsetFormat = Expression.Constant(DateTimeOffsetFormatPattern);

            var concatMethod = typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });
            var entityType = typeof(TModel);

            ParameterExpression arg = Expression.Parameter(entityType, "x");
            Expression selector = null;

            request.OrderBy.ToList().ForEach(field =>
            {
                var propertyInfo = entityType.GetProperty(field);
                Expression property = Expression.Property(arg, field);

                if (propertyInfo.PropertyType.IsGenericType &&
                    propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var underlyingType = propertyInfo.PropertyType.GetGenericArguments()[0];

                    property = Expression.Convert(
                        Expression.Coalesce(property, Expression.Default(underlyingType)),
                        underlyingType);
                }

                if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                {
                    property = Expression.Call(property, dateTimeToString, dateTimeFormat);
                }
                else if (propertyInfo.PropertyType == typeof(DateTimeOffset) || propertyInfo.PropertyType == typeof(DateTimeOffset?))
                {
                    property = Expression.Call(property, dateTimeOffsetToString, dateTimeOffsetFormat);
                }
                else if (propertyInfo.PropertyType != typeof(string))
                {
                    property = Expression.Call(property, objectToString);
                }

                if (selector != null)
                {
                    selector = Expression.Add(
                        selector,
                        property,
                        concatMethod);
                }
                else
                {
                    selector = property;
                }
            });

            return (Func<TModel, object>)Expression.Lambda(selector, new ParameterExpression[] { arg }).Compile();
        }

        public static Type GetCursorType<TModel>(this IPaginationInput<TModel> request)
        {
            if (request.OrderBy == null || request.OrderBy.Length == 0)
            {
                throw new Exception("Order by is not specified");
            }

            if (request.OrderBy.Length > 1)
            {
                return typeof(string);
            }

            var model = typeof(TModel);
            var propertyInfo = model.GetProperty(request.OrderBy.First());

            if (propertyInfo == null)
            {
                throw new Exception("The specified order by column does not exist");
            }

            return propertyInfo.PropertyType;
        }
    }
}
