using System;
using System.Collections.Generic;

namespace CoronavirusDailyReportApp.Core.Models {
    public class CovidRequestPayloadNew {
        public CovidRequestPayloadNew (List<NewLocation> locations) {
            Locations = locations;
        }

        public List<NewLocation> Locations { get; set; }

    }
    public class NewLocation {
        public NewLocation (int id, string country, List<CovidStats> dailyStats) {
            Id = id;
            Country = country;
            DailyStats = dailyStats;
        }

        public int Id { get; set; }
        public string Country { get; set; }
        public List<CovidStats> DailyStats { get; set; }

    }
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