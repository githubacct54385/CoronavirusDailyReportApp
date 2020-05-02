using System.Collections.Generic;

namespace CoronavirusDailyReportApp.Core.Models {
    public class Location {
        public Location (int id, string country, List<CovidStats> dailyStats) {
            Id = id;
            Country = country;
            DailyStats = dailyStats;
        }

        public int Id { get; set; }
        public string Country { get; set; }
        public List<CovidStats> DailyStats { get; set; }

    }
}