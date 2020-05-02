using System;

namespace CoronavirusDailyReportApp.Core.Models {
    public class TimelineData {
        public int CountryId { get; set; }
        public DateTime TimelineDate { get; set; }
        public int Deaths { get; set; }
        public int Confirmed { get; set; }

        public TimelineData (int countryId, DateTime timelineDate, int deaths, int confirmed) {
            this.CountryId = countryId;
            this.TimelineDate = timelineDate;
            this.Deaths = deaths;
            this.Confirmed = confirmed;
        }
    }
}