using Ciemesus.Core.Api.Infrastructure;
using Ciemesus.Core.Authentication;
using Ciemesus.Core.Data;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace Ciemesus.Core.Validation
{
    public class HasPrincipalAccessToUserValidator : PropertyValidator
    {
        private readonly CiemesusDb _db;
        private readonly CiemesusPrincipal _principal;

        public HasPrincipalAccessToUserValidator(CiemesusDb db, CiemesusPrincipal principal) : base("{PropertyName} value is invalid.")
        {
            _db = db;
            _principal = principal;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var valid = false;

            var userId = (int)context.PropertyValue;

            var user = _db.Users
                .Include(u => u.ApplicationRoles)
                .FirstOrDefault(u => u.UserId == userId);

            var isPrincipalInHigherRole = _principal.Claims
                           .Where(x => x.Type == ClaimTypes.Role)
                           .Any(x => x.Value == RoleEnum.SuperAdministrator.ToString()
                           || x.Value == RoleEnum.Administrator.ToString()
                           || x.Value == RoleEnum.PublicAdministrator.ToString());

            if (isPrincipalInHigherRole)
            {
                valid = true;
            }

            return valid;
        }
    }
}
