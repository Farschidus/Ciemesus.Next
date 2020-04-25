using Ciemesus.Core.Extensions;
using System;

namespace Ciemesus.Core.Data.Infrastructure
{
    public abstract class Audit
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public void UpdateAuditProperties(string timezoneId = null)
        {
            var dateTime = timezoneId == null ?
                DateTime.UtcNow.ToLocalSiteDateTimeOffset() :
                DateTime.UtcNow.ToLocalSiteDateTimeOffset(timezoneId);

            if (CreatedAt == DateTimeOffset.MinValue)
            {
                CreatedAt = dateTime;
            }

            UpdatedAt = dateTime;
        }
    }
}
