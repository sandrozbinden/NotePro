using System;
using System.Globalization;

namespace NotePro.ExtensionMethods
{
    public static class DateTimeToTextExtensionMethod
    {
        private const int Second = 1;
        private const int Minute = 60 * Second;
        private const int Hour = 60 * Minute;
        private const int Day = 24 * Hour;
        private const int Month = 30 * Day;

        public static String ToText(this DateTime date)
        {
            if (date.Date < DateTime.Today.AddDays(-30))
            {
                return "In the Past";
            }
            else if (date.Date < DateTime.Today.AddDays(-1))
            {
                return (DateTime.Today - date).TotalDays  + " Days ago";
            }
            else if (date.Date == DateTime.Today.AddDays(-1))
            {
                return "Yesterday";
            }
            else if (date.Date == DateTime.Today)
            {
                return "Today";
            }
            else if (date.Date == DateTime.Today.AddDays(1))
            {
                return "Tomorrow";
            }
            else if (date.Date < DateTime.Today.AddDays(30))
            {
                return "In " + (date - DateTime.Today).TotalDays + " Days";
            }
            else
            {
                return "In the Future";
            }

        }

        private static int GetWeekOfYear(DateTime date)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar calendar = dfi.Calendar;
            return calendar.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }
    }
}
