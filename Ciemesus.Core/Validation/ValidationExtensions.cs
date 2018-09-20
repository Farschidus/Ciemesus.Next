using Ciemesus.Core.Data;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using System.Collections.Generic;

namespace Ciemesus.Core.Validation
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, TElement> CiemesusExists<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, CiemesusDb db)
        {
            return ruleBuilder.SetValidator(new CiemesusExistsValidator(db));
        }

        public static IRuleBuilderOptions<T, TElement> MemberExists<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, CiemesusDb db)
        {
            return ruleBuilder.SetValidator(new MemberExistsValidator(db));
        }

        public static IRuleBuilderOptions<T, TElement> TeamExists<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, CiemesusDb db)
        {
            return ruleBuilder.SetValidator(new TeamExistsValidator(db));
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
