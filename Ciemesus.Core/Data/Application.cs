using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ciemesus.Core.Data
{
    public class Application
    {
        [Key, Column("Application"), StringLength(50)]
        public string Name { get; set; }
        [Required, Column("ApplicationName"), StringLength(200)]
        public string DisplayName { get; set; }

        public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }
    }
}
