using System.Security.Claims;

namespace Ciemesus.Core.Authentication
{
    public class CiemesusPrincipal : ClaimsPrincipal
    {
        public CiemesusPrincipal(ClaimsPrincipal principal) : base(principal)
        {
            var id = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(id))
            {
                if (int.TryParse(id, out int userId))
                {
                    UserId = userId;
                }
            }

            var email = principal.FindFirstValue(ClaimTypes.Email);

            if (!string.IsNullOrEmpty(email))
            {
                Email = email;
            }

            var application = principal.FindFirstValue("Application");

            if (!string.IsNullOrEmpty(application))
            {
                Application = application;
            }
        }

        public int UserId { get; set; }

        public string Email { get; set; }

        public string Application { get; set; }
    }
}
