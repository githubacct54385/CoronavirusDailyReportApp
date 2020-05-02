using System;
using System.Collections.Generic;

namespace CoronavirusDailyReportApp.Core.DateLocalization {
    public class DateLocalizer {

        public DateLocalizer () {
            TimeZoneKvp = InitTzKvp ();
        }

        private List<KeyValuePair<int, string>> InitTzKvp () {
            List<KeyValuePair<int, string>> tzKvp = new List<KeyValuePair<int, string>> ();
            tzKvp.Add (new KeyValuePair<int, string> (0, "UTC"));
            tzKvp.Add (new KeyValuePair<int, string> (8, "SGT"));
            return tzKvp;
        }

        public DateTime LocalizeDate (DateTime inputDateUtc, int hours) {
            return inputDateUtc.AddHours (hours);
        }

        public string LocalizeDateToString (DateTime inputDateUtc, int hours, string pattern) {
            return inputDateUtc.AddHours (hours).ToString (pattern) + " " + TimeZoneName (hours);
        }

        private List<KeyValuePair<int, string>> TimeZoneKvp { get; set; }
        private string TimeZoneName (int hours) {
            return TimeZoneKvp.Find (p => p.Key == hours).Value;
        }
    }
}