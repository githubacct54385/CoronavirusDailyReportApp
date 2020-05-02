using System;

namespace CoronavirusDailyReportApp.Core.Models {
    public class CovidStats {
        public CovidStats (int confirmed, int deaths, DateTime day, bool isLatest) {
            Confirmed = confirmed;
            Deaths = deaths;
            Day = day;
            IsLatest = isLatest;
        }

        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public DateTime Day { get; set; }
        public bool IsLatest { get; set; }
    }
}