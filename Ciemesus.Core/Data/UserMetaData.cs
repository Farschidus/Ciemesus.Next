using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ciemesus.Core.Data
{
    public class UserMetaData
    {
        public const int UserIdMaxLength = 450;
        public const int FirstNameMaxLength = 32;
        public const int LastNameMaxLength = 32;

        [Key]
        [Required]
        public int UserMetaDataId { get; set; }

        [Required]
        [StringLength(UserIdMaxLength)]
        public string UserId { get; set; }

        [Required]
        [StringLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(LastNameMaxLength)]
        public string LastName { get; set; }

        public string Pic { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
