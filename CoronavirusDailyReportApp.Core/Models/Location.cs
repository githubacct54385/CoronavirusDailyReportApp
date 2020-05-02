using System;
using System.Collections.Generic;

namespace CoronavirusDailyReportApp.Core.Models {
    public class Location {
        public Location (int id, string country, DateTime lastUpdatedTime, List<TimelineData> timelineData) {
            Id = id;
            Country = country;
            LastUpdatedTime = lastUpdatedTime;
            TimelineData = timelineData;
        }

        public int Id { get; set; }
        public string Country { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public List<TimelineData> TimelineData { get; set; }

    }
}