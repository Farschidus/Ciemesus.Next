using Ciemesus.Core.Authentication;
using FluentValidation.Validators;
using System;
using System.Linq;

namespace Ciemesus.Core.Api.Infrastructure.Validation
{
    public class IsCurrentApplicationAllowedValidator : PropertyValidator
    {
        private readonly CiemesusPrincipal _principal;
        private readonly ApplicationEnum[] _allowedApplication;

        public IsCurrentApplicationAllowedValidator(CiemesusPrincipal principal, ApplicationEnum[] allowedApplications)
             : base("The logged in user does not have access to this resource")
        {
            _principal = principal;
            _allowedApplication = allowedApplications;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var principalApplication = _principal.Claims
                    .Where(x => x.Type == "Application")
                    .Select(x => x.Value)
                    .FirstOrDefault();

            if (Enum.TryParse(principalApplication, out ApplicationEnum application))
            {
                return _allowedApplication.Contains(application);
            }

            return false;
        }
    }
}
