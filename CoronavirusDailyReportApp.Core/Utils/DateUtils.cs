using System;

namespace CoronavirusDailyReportApp.Core.Utils {
    public static class DateUtils {
        public static string GetDateToken (DateTime date) {
            string year = GetYear (date);
            string month = GetMonth (date);
            string day = GetDay (date);
            string dayAsString = GetDayAsString (year, month, day);
            return dayAsString;
        }

        private static string GetYear (DateTime date) => date.ToString ("yyyy");
        private static string GetMonth (DateTime date) => date.ToString ("MM");
        private static string GetDay (DateTime date) => date.ToString ("dd");
        private static string GetDayAsString (string year, string month, string day) => $"{year}-{month}-{day}T00:00:00Z";
    }
}