using Ciemesus.Core.Data;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using System.Collections.Generic;

namespace Ciemesus.Core.Validation
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, TElement> UserExists<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, CiemesusDb db)
        {
            return ruleBuilder.SetValidator(new UserExistsValidator(db));
        }

        public static void AddFailures(this CustomContext context, IList<ValidationFailure> failures)
        {
            foreach (var failure in failures)
            {
                context.AddFailure(failure);
            }
        }
    }
}
