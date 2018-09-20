using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ciemesus.Core.Data
{
    public class Team
    {
        public const int TeamNameMaxLength = 128;

        [Required]
        public int TeamId { get; set; }

        [Required]
        [StringLength(TeamNameMaxLength)]
        public string TeamName { get; set; }

        [Required]
        public DateTime StartedAt { get; set; }

        public int Interval { get; set; }

        public string Pics { get; set; }
    }
}
