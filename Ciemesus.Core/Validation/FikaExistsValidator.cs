using System.Linq;
using Ciemesus.Core.Data;
using FluentValidation.Validators;

namespace Ciemesus.Core.Validation
{
    public class CiemesusExistsValidator : PropertyValidator
    {
        private CiemesusDb _db;

        public CiemesusExistsValidator(CiemesusDb db) : base("{PropertyName} value is invalid.")
        {
            _db = db;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var valid = false;

            var fikaId = (int?)context.PropertyValue;

            if (fikaId.HasValue)
            {
                var exists = _db.Subject.Any(p => p.CiemesusId == fikaId.Value);

                if (exists)
                {
                    valid = true;
                }
            }

            return valid;
        }
    }
}
