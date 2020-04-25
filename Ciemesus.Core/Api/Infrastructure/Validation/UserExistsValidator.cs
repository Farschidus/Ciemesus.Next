using Ciemesus.Core.Data;
using FluentValidation.Validators;
using System.Linq;

namespace Ciemesus.Core.Validation
{
    public class UserExistsValidator : PropertyValidator
    {
        private readonly CiemesusDb _db;

        public UserExistsValidator(CiemesusDb db) : base("{PropertyName} value is invalid.")
        {
            _db = db;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var valid = false;

            var userId = (int?)context.PropertyValue;

            if (userId.HasValue)
            {
                var exists = _db.Users.Any(u => u.UserId == userId.Value);

                if (exists)
                {
                    valid = true;
                }
            }

            return valid;
        }
    }
}
