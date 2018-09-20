using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ciemesus.Core.Data
{
    public class Language
    {
        public const int LanguageTitleMaxLength = 64;
        public const int LanguageCodeMaxLength = 32;

        [Key]
        [Required]
        public byte LanguageId { get; set; }

        [Required]
        [StringLength(LanguageTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(LanguageCodeMaxLength)]
        public string Code { get; set; }

        public bool IsRTL { get; set; }

        public bool IsActive { get; set; }

        public byte Order { get; set; }
    }
}
