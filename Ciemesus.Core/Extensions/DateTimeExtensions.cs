using Ciemesus.Core.Utilities;
using System;
using TimeZoneConverter;

namespace Ciemesus.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public const string UTC = "UTC";

        // Convert the given datetime into Utc format
        public static DateTime ToUtcFormat(this DateTime date)
        {
            return TimeZoneInfo.ConvertTimeToUtc(date, TimeZoneInfo.Utc);
        }

        public static DateTime ToLocalSiteDateTimeFormat(this DateTime date, string siteTimeZoneId = UTC)
        {
            var siteTimeZoneInfo = GetTimeZoneInfo(siteTimeZoneId);
            Check.Reference.IsNotNull(siteTimeZoneInfo, nameof(siteTimeZoneInfo));

            return TimeZoneInfo.ConvertTime(date, siteTimeZoneInfo);
        }

        // Convert the given Utc datetime to Utc datetimeoffset
        public static DateTimeOffset ToUtcDateTimeOffset(this DateTime utcTime)
        {
            return DateTime.SpecifyKind(utcTime, DateTimeKind.Utc);
        }

        // Convert the given datetime into local site timezone datetime
        public static DateTime ToLocalSiteDateTime(this DateTime utcTime, string siteTimeZoneId = UTC)
        {
            var siteTimeZoneInfo = GetTimeZoneInfo(siteTimeZoneId);
            Check.Reference.IsNotNull(siteTimeZoneInfo, nameof(siteTimeZoneInfo));

            DateTime localSiteTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, siteTimeZoneInfo);

            return localSiteTime;
        }

        // Convert the given Utc datetime to local site timezone datetimeoffset
        public static DateTimeOffset ToLocalSiteDateTimeOffset(this DateTime utcTime, string siteTimeZoneId = UTC)
        {
            var siteTimeZoneInfo = GetTimeZoneInfo(siteTimeZoneId);
            Check.Reference.IsNotNull(siteTimeZoneInfo, nameof(siteTimeZoneInfo));

            DateTime localSiteTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, siteTimeZoneInfo);
            DateTimeOffset localSiteDateTimeOffset = new DateTimeOffset(localSiteTime, siteTimeZoneInfo.GetUtcOffset(localSiteTime));

            return localSiteDateTimeOffset;
        }

        // Add site offset to a local site datetime
        public static DateTimeOffset AddSiteOffset(this DateTime siteTime, string siteTimeZoneId = UTC)
        {
            var siteTimeZoneInfo = GetTimeZoneInfo(siteTimeZoneId);
            Check.Reference.IsNotNull(siteTimeZoneInfo, nameof(siteTimeZoneInfo));

            siteTime = DateTime.SpecifyKind(siteTime, DateTimeKind.Unspecified);
            DateTimeOffset localSiteDateTimeOffset = new DateTimeOffset(siteTime, siteTimeZoneInfo.GetUtcOffset(siteTime));

            return localSiteDateTimeOffset;
        }

        // Set a datetimeoffset to the start of the day
        public static DateTimeOffset ToStartOfDay(this DateTimeOffset dateTime)
        {
            return new DateTimeOffset(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0, dateTime.Offset);
        }

        // Set a datetimeoffset to the end of the day
        public static DateTimeOffset ToEndOfDay(this DateTimeOffset dateTime)
        {
            return new DateTimeOffset(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 999, dateTime.Offset);
        }

        public static DateTimeOffset ToStartOfMonth(this DateTimeOffset dateTime)
        {
            return dateTime.ToStartOfDay().AddDays(1 - dateTime.Day);
        }

        public static DateTimeOffset ToStartOfNextMonthPowerBi(this DateTimeOffset dateTime)
        {
            return dateTime.ToStartOfMonth().AddMonths(1);
        }

        public static double TimeDifferenceInHours(this DateTimeOffset? dateTime)
        {
            var universalTime = dateTime.Value.ToUniversalTime();
            var currentUniversalDateTime = DateTimeOffset.UtcNow.ToUniversalTime();
            var timeDifferenceInHours = Math.Abs((universalTime - currentUniversalDateTime).TotalHours);
            return timeDifferenceInHours;
        }

        public static string ToShortDayOfWeek(this DateTimeOffset dateTime)
        {
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    {
                        return "sun";
                    }

                case DayOfWeek.Monday:
                    {
                        return "mon";
                    }

                case DayOfWeek.Tuesday:
                    {
                        return "tue";
                    }

                case DayOfWeek.Wednesday:
                    {
                        return "wed";
                    }

                case DayOfWeek.Thursday:
                    {
                        return "thu";
                    }

                case DayOfWeek.Friday:
                    {
                        return "fri";
                    }

                case DayOfWeek.Saturday:
                    {
                        return "sat";
                    }

                default:
                    {
                        return string.Empty;
                    }
            }
        }

        private static TimeZoneInfo GetTimeZoneInfo(string siteTimeZoneId)
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                return TimeZoneInfo.FindSystemTimeZoneById(TZConvert.WindowsToIana(siteTimeZoneId));
            }

            return TimeZoneInfo.FindSystemTimeZoneById(siteTimeZoneId);
        }
    }
}
