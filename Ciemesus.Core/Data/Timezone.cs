using System.ComponentModel.DataAnnotations;

namespace Ciemesus.Core.Data
{
    public class Timezone
    {
        [Required, Key]
        public string TimezoneId { get; set; }

        [Required]
        public int BaseUtcOffsetInMinutes { get; set; }

        public string DisplayName { get; set; }
    }
}
