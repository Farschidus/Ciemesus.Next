using System.Linq;
using Ciemesus.Core.Data;
using FluentValidation.Validators;

namespace Ciemesus.Core.Validation
{
    public class TeamExistsValidator : PropertyValidator
    {
        private CiemesusDb _db;

        public TeamExistsValidator(CiemesusDb db) : base("{PropertyName} value is invalid.")
        {
            _db = db;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var valid = false;

            var teamId = (int?)context.PropertyValue;

            if (teamId.HasValue)
            {
                var exists = _db.Teams.Any(p => p.TeamId == teamId.Value);

                if (exists)
                {
                    valid = true;
                }
            }

            return valid;
        }
    }
}
