using System;

namespace CoronavirusDailyReportApp.Core.Models {
    public class CovidStats {
        public CovidStats (int confirmed, int deaths, DateTime day) {
            this.Confirmed = confirmed;
            this.Deaths = deaths;
            this.Day = day;

        }
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public DateTime Day { get; set; }
    }
}