using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ciemesus.Core.Data
{
    public class UserApplicationRole
    {
        [Column(Order = 1)]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Column(Order = 2), StringLength(50)]
        public string Application { get; set; }

        [Column(Order = 3), StringLength(50)]
        public string Role { get; set; }
    }
}
