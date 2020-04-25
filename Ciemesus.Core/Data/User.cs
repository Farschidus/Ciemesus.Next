using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ciemesus.Core.Data
{
    public class User
    {
        public const int NameMaxLength = 256;
        public const int EmailMaxLength = 256;
        public const int UsernameMaxLength = 256;
        public const int PasswordMinLength = 8;
        public const int DefaultUserId = 1;
        public const int LocaleMinLength = 2;
        public const int LocaleMaxLength = 20;

        [Key]
        public int UserId { get; set; }

        [Required, StringLength(NameMaxLength)]
        public string Name { get; set; }

        [StringLength(EmailMaxLength)]
        public string Email { get; set; }

        [StringLength(UsernameMaxLength)]
        public string Username { get; set; }

        public Guid? IdentityProviderUserId { get; set; }

        [NotMapped, MinLength(PasswordMinLength)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<UserApplicationRole> ApplicationRoles { get; set; }
    }
}
