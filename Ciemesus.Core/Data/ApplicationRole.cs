using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ciemesus.Core.Data
{
    public class ApplicationRole
    {
        public const int RoleMaxLength = 50;
        public const int RoleNameMaxLength = 200;

        [Column(Order = 1), StringLength(50)]
        public string Application { get; set; }
        [Column(Order = 2), StringLength(RoleMaxLength)]
        public string Role { get; set; }
        [Required, StringLength(RoleNameMaxLength)]
        public string RoleName { get; set; }
    }
}
