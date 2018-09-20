using System;
using System.ComponentModel.DataAnnotations;

namespace Ciemesus.Core.Data
{
    public class CiemesusComment
    {
        public const int CiemesusCommentMaxLength = 512;

        [Required]
        public int CiemesusCommentId { get; set; }

        [Required]
        public int CiemesusId { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        [StringLength(CiemesusCommentMaxLength)]
        public string Comment { get; set; }

        [Required]
        public DateTime CommentDate { get; set; }

        public int Likes { get; set; }

        public virtual Ciemesus Ciemesus { get; set; }

        public virtual Member Member { get; set; }
    }
}
