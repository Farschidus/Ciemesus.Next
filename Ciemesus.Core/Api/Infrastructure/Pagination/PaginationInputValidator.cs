using FluentValidation;
using System;
using System.Linq;

namespace Ciemesus.Core.Api.Infrastructure.Pagination
{
    public class PaginationInputValidator<T> : AbstractValidator<IPaginationInput<T>>
    {
        public PaginationInputValidator()
        {
            RuleFor(x => x.First)
                .NotEmpty();

            RuleFor(x => x.OrderBy)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .Must((rootObject, fields) =>
                {
                    var model = typeof(T);

                    var allExists = fields.ToList().Exists((field) =>
                    {
                        var propertyInfo = model.GetProperty(field);
                        if (propertyInfo == null || Attribute.IsDefined(propertyInfo, typeof(OrderableAttribute)) == false)
                        {
                            return false;
                        }

                        return true;
                    });

                    return allExists;
                })
                .WithMessage("Cannot order by one or more of the requested fields");

            RuleFor(x => x.Before)
                .Empty()
                .When(x =>
                {
                    if (x.OrderBy == null || x.OrderBy.Length == 0)
                    {
                        return false;
                    }

                    Type type = null;
                    try
                    {
                        type = x.GetCursorType();
                    }
                    catch (Exception)
                    { }

                    return type != null && x.After != null && type != typeof(string) && !x.After.Equals(Activator.CreateInstance(type));
                });

            RuleFor(x => x.After)
                .Empty()
                .When(x =>
                {
                    if (x.OrderBy == null || x.OrderBy.Length == 0)
                    {
                        return false;
                    }

                    Type type = null;
                    try
                    {
                        type = x.GetCursorType();
                    }
                    catch (Exception)
                    { }

                    return type != null && x.Before != null && type != typeof(string) && !x.Before.Equals(Activator.CreateInstance(type));
                });
        }
    }
}
