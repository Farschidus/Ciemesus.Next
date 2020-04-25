using Ciemesus.Core.Authentication;
using FluentValidation.Validators;
using System.Linq;
using System.Security.Claims;

namespace Ciemesus.Core.Api.Infrastructure.Validation
{
    public class HasRoleValidator : PropertyValidator
    {
        private readonly CiemesusPrincipal _principal;
        private readonly RoleEnum[] _requiredRoles;

        public HasRoleValidator(CiemesusPrincipal principal, RoleEnum[] requiredRoles)
             : base("The logged in user does not have access to this resource")
        {
            _principal = principal;
            _requiredRoles = requiredRoles;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var valid = true;
            var roles = _principal.Claims
                .Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value)
                .ToList();

            _requiredRoles.ToList().ForEach(x =>
            {
                switch (x)
                {
                    default:
                        if (roles.Any(y => y == x.ToString()) == false)
                        {
                            valid = false;
                        }

                        break;
                }
            });

            return valid;
        }
    }
}
