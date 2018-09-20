using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ciemesus.Core.Data
{
    public class TeamMember
    {
        [Required]
        [Key]
        [Column(Order = 1)]
        public int TeamId { get; set; }

        [Required]
        [Key]
        [Column(Order = 2)]
        public int MemberId { get; set; }

        public int MemberOrder { get; set; }

        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
    }
}
