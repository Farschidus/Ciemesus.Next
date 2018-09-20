using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ciemesus.Core.Data
{
    public class Subject
    {
        public const int SubjectSlugMaxLength = 128;
        public const int SubjectTitleMaxLength = 256;
        public const int SubjectExcerptMaxLength = 512;
        public const int SubjectMimeTypetMaxLength = 64;

        [Key]
        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ParentId { get; set; }

        [Required]
        public byte LanguageId { get; set; }

        [Required]
        public byte Type { get; set; }

        [Required]
        [StringLength(SubjectSlugMaxLength)]
        public string Slug { get; set; }

        [Required]
        [StringLength(SubjectTitleMaxLength)]
        public string Title { get; set; }

        public string Content { get; set; }

        [StringLength(SubjectExcerptMaxLength)]
        public string Excerpt { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(SubjectMimeTypetMaxLength)]
        public string MimeType { get; set; }

        public byte Status { get; set; }

        public int Count { get; set; }

        public int Order { get; set; }
    }
}
