using System.Linq;
using Ciemesus.Core.Data;
using FluentValidation.Validators;

namespace Ciemesus.Core.Validation
{
    public class MemberExistsValidator : PropertyValidator
    {
        private CiemesusDb _db;

        public MemberExistsValidator(CiemesusDb db) : base("{PropertyName} value is invalid.")
        {
            _db = db;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var valid = false;

            var memberId = (int?)context.PropertyValue;

            if (memberId.HasValue)
            {
                var exists = _db.Members.Any(p => p.MemberId == memberId.Value);

                if (exists)
                {
                    valid = true;
                }
            }

            return valid;
        }
    }
}
