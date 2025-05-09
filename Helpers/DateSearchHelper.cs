using System;

namespace Stream.Helpers
{
    public static class DateSearchHelper
    {
        public static bool IsYearMonthMatch(DateTime createdAt, string searchQuery)
        {
            var parts = searchQuery.Split('-');
            if (parts.Length == 2 && int.TryParse(parts[0], out var year) && int.TryParse(parts[1], out var month))
            {
                return createdAt.Year == year && createdAt.Month == month;
            }
            return false;
        }

        public static bool IsMonthDayMatch(DateTime createdAt, string searchQuery)
        {
            var parts = searchQuery.Split('-');
            if (parts.Length == 2 && int.TryParse(parts[0], out var month) && int.TryParse(parts[1], out var day))
            {
                return createdAt.Month == month && createdAt.Day == day;
            }
            return false;
        }

        public static bool IsExactDateMatch(DateTime createdAt, string searchQuery)
        {
            if (DateTime.TryParse(searchQuery, out var parsedDate))
            {
                return createdAt.Date == parsedDate.Date;
            }
            return false;
        }

        public static bool IsTimeMatch(DateTime createdAt, string searchQuery)
        {
            if (TimeSpan.TryParse(searchQuery, out var parsedTime))
            {
                return createdAt.TimeOfDay == parsedTime;
            }
            return false;
        }

        public static bool IsHourOrMinuteMatch(DateTime createdAt, string searchQuery)
        {
            if (int.TryParse(searchQuery, out var value))
            {
                return createdAt.Hour == value || createdAt.Minute == value;
            }
            return false;
        }
    }
}