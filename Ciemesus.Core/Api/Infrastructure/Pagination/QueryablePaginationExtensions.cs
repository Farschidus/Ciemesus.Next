using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ciemesus.Core.Api.Infrastructure.Pagination
{
    public static class QueryablePaginationExtensions
    {
        public static async Task<IQueryable<TQuery>> SetPaging<TQuery, TModel>(this IQueryable<TQuery> query, Pagination<TModel> pagination, IPaginationInput<TModel> request)
        {
            query = query.ApplyFilter(request);
            pagination.TotalCount = await query.CountAsync();
            var isAsc = request.IsAsc;

            var cursorType = request.GetCursorType();
            var defaultValue = cursorType == typeof(string) ? string.Empty : Activator.CreateInstance(cursorType);
            var isBefore = request.Before != null && request.Before != defaultValue;
            var isAfter = request.After != null && request.After != defaultValue;

            var check1 = false;

            if (isAfter || isBefore)
            {
                query = query
                    .Where(request);

                check1 = await query
                    .Where(request).AnyAsync();
            }

            var check2 = await (isAsc ? query.OrderBy(request) : query.OrderByDescending(request))
                .Skip(request.First).AnyAsync();

            if (isBefore)
            {
                pagination.HasNextPage = check1;
                pagination.HasPreviousPage = check2;
            }
            else
            {
                pagination.HasNextPage = check2;
                pagination.HasPreviousPage = check1;
            }

            query = isAsc ? query.OrderBy(request) : query.OrderByDescending(request);

            if (isBefore)
            {
                var count = await query.CountAsync();
                return query.Skip(count > request.First ? count - request.First : 0);
            }

            return query.Take(request.First);
        }

        public static IOrderedQueryable<TQuery> OrderBy<TQuery, TModel>(this IQueryable<TQuery> query, IPaginationInput<TModel> request)
        {
            return GetOrderBy(query, request, "OrderBy");
        }

        public static IOrderedQueryable<TQuery> OrderByDescending<TQuery, TModel>(this IQueryable<TQuery> query, IPaginationInput<TModel> request)
        {
            return GetOrderBy(query, request, "OrderByDescending");
        }

        public static IQueryable<TQuery> Where<TQuery, TModel>(this IQueryable<TQuery> query, IPaginationInput<TModel> request)
        {
            var isAsc = request.IsAsc;
            var cursorType = request.GetCursorType();
            var entityType = typeof(TQuery);
            var defaultValue = cursorType == typeof(string) ? string.Empty : Activator.CreateInstance(cursorType);

            var isBefore = request.Before != null && request.Before != defaultValue;
            var isAfter = request.After != null && request.After != defaultValue;
            var isGreaterThan = isBefore ? !isAsc : isAsc;

            var value = isBefore ? request.Before : request.After;
            if (cursorType == typeof(Guid))
            {
                value = new Guid(value as string);
            }
            else
            {
                var converter = TypeDescriptor.GetConverter(Nullable.GetUnderlyingType(cursorType) ?? cursorType);
                value = value.GetType() == typeof(string) ?
                    converter.ConvertFrom(value) :
                    Convert.ChangeType(value, Nullable.GetUnderlyingType(cursorType) ?? cursorType);
            }

            var compareToExpression = Expression.Constant(value);

            var concatMethod = typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            var selector = GetLambda<TQuery, TModel>(request, arg);

            if (cursorType == typeof(string) || cursorType == typeof(Guid))
            {
                var compareTo = cursorType == typeof(string)
                    ? typeof(string).GetMethod(nameof(string.CompareTo), new[] { typeof(string) })
                    : typeof(Guid).GetMethod(nameof(string.CompareTo), new[] { typeof(Guid) });
                selector = Expression.Call(selector, compareTo, compareToExpression);
                compareToExpression = Expression.Constant(0);
            }

            var clause = Expression.Lambda(
                isGreaterThan
                    ? NullableGreaterThan(selector, compareToExpression)
                    : NullableLessThan(selector, compareToExpression),
                new ParameterExpression[] { arg });

            var method = GetWhereMethod();

            MethodInfo genericMethod = method
                .MakeGenericMethod(entityType);

            var newQuery = (IQueryable<TQuery>)genericMethod
                .Invoke(genericMethod, new object[] { query, clause });
            return newQuery;
        }

        public static Expression NullableGreaterThan(Expression e1, Expression e2)
        {
            if (IsNullableType(e1.Type) && !IsNullableType(e2.Type))
            {
                e2 = Expression.Convert(e2, e1.Type);
            }
            else if (!IsNullableType(e1.Type) && IsNullableType(e2.Type))
            {
                e1 = Expression.Convert(e1, e2.Type);
            }

            return Expression.GreaterThan(e1, e2);
        }

        public static Expression NullableLessThan(Expression e1, Expression e2)
        {
            if (IsNullableType(e1.Type) && !IsNullableType(e2.Type))
            {
                e2 = Expression.Convert(e2, e1.Type);
            }
            else if (!IsNullableType(e1.Type) && IsNullableType(e2.Type))
            {
                e1 = Expression.Convert(e1, e2.Type);
            }

            return Expression.LessThan(e1, e2);
        }

        public static bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static Expression GetLambda<TQuery, TModel>(IPaginationInput<TModel> request, ParameterExpression arg)
        {
            var convertToString = typeof(object).GetMethod("ToString");
            var concatMethod = typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });
            var modelEntityType = typeof(TModel);
            var queryEntityType = typeof(TQuery);

            Expression selector = null;
            request.OrderBy.ToList().ForEach(field =>
            {
                var targetFieldName = modelEntityType.GetProperty(field).GetCustomAttribute<OrderableAttribute>().MapsTo;
                var propertyInfo = queryEntityType.GetProperty(targetFieldName);
                Expression property = Expression.Property(arg, targetFieldName);

                if (propertyInfo.PropertyType != typeof(string) && request.OrderBy.Length > 1)
                {
                    property = Expression.Call(property, convertToString);
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
            return selector;
        }

        public static IOrderedQueryable<TQuery> GetOrderBy<TQuery, TModel>(this IQueryable<TQuery> query, IPaginationInput<TModel> request, string methodName)
        {
            var cursorType = request.GetCursorType();
            var entityType = typeof(TQuery);
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            var selector = Expression.Lambda(GetLambda<TQuery, TModel>(request, arg), new ParameterExpression[] { arg });
            var enumarableType = typeof(Queryable);
            var method = enumarableType.GetMethods()
                .Where(m => m.Name == methodName && m.IsGenericMethodDefinition)
                .Where(m =>
                {
                    var parameters = m.GetParameters().ToList();
                    return parameters.Count == 2;
                })
                .Single();
            MethodInfo genericMethod = method
                .MakeGenericMethod(entityType, cursorType);

            var newQuery = (IOrderedQueryable<TQuery>)genericMethod
                .Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }

        public static IQueryable<TQuery> ApplyFilter<TQuery, TModel>(this IQueryable<TQuery> query, IFilterableInput<TModel> request)
        {
            if (string.IsNullOrEmpty(request.Filter))
            {
                return query;
            }

            var convertToStringMethod = typeof(object).GetMethod("ToString");
            var concatMethod = typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var modelEntityType = typeof(TModel);
            var queryEntityType = typeof(TQuery);
            var arg = Expression.Parameter(queryEntityType, "x");
            var compareToExpression = Expression.Constant(request.Filter);

            Expression clause = null;
            modelEntityType
                .GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(FilterableAttribute)))
                .ToList()
                .ForEach(field =>
                {
                    var targetFieldName = field.GetCustomAttribute<FilterableAttribute>().MapsTo;
                    var propertyInfo = queryEntityType.GetProperty(targetFieldName);
                    Expression property = Expression.Property(arg, targetFieldName);

                    if (propertyInfo.PropertyType != typeof(string))
                    {
                        property = Expression.Call(property, convertToStringMethod);
                    }

                    property = Expression.Call(property, containsMethod, compareToExpression);

                    if (clause != null)
                    {
                        clause = Expression.Or(clause, property);
                    }
                    else
                    {
                        clause = property;
                    }
                });

            if (clause == null)
            {
                return query;
            }

            clause = Expression.Lambda(clause, arg);

            var method = GetWhereMethod();

            MethodInfo genericMethod = method
                .MakeGenericMethod(queryEntityType);

            var newQuery = (IQueryable<TQuery>)genericMethod
                .Invoke(genericMethod, new object[] { query, clause });
            return newQuery;
        }

        private static MethodInfo GetWhereMethod()
        {
            var enumarableType = typeof(Queryable);
            return enumarableType.GetMethods()
                .Where(m => m.Name == "Where" && m.IsGenericMethodDefinition)
                .Where(m =>
                {
                    var parameters = m.GetParameters().ToList();
                    return parameters.Count == 2;
                })
                .First();
        }
    }
}
