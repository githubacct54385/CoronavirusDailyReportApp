using System;
using System.Collections.Generic;
using CoronavirusDailyReportApp.Core.Models;

namespace CoronavirusAzFunction.Tests {
    public static class SampleTestData {
        public static List<Location> SampleLocations () {

            List<Location> locations = new List<Location> ();

            List<TimelineData> usaStats = new List<TimelineData> ();
            // usaStats.Add(new TimelineData(225, new DateTime(2020, 4, 30), ))
            usaStats.Add (new TimelineData (225, new DateTime (2020, 4, 30), 1000000, 55000));
            usaStats.Add (new TimelineData (225, new DateTime (2020, 4, 29), 950000, 45000));

            List<TimelineData> singaporeStats = new List<TimelineData> ();
            singaporeStats.Add (new TimelineData (196, new DateTime (2020, 4, 30), 50, 5));
            singaporeStats.Add (new TimelineData (196, new DateTime (2020, 4, 30), 30, 3));

            locations.Add (new Location (id: 1, country: "USA", lastUpdatedTime : new DateTime (2020, 4, 30), timelineData : usaStats));
            locations.Add (new Location (id: 2, country: "Singapore", lastUpdatedTime : new DateTime (2020, 4, 30), timelineData : singaporeStats));
            return locations;
        }

        public static List<Location> SampleNegativeChangeLocations () {

            List<Location> locations = new List<Location> ();

            List<TimelineData> usaStats = new List<TimelineData> ();
            usaStats.Add (new TimelineData (225, new DateTime (2020, 4, 30), 175000, 75000));
            usaStats.Add (new TimelineData (225, new DateTime (2020, 4, 29), 200000, 75000));

            List<TimelineData> singaporeStats = new List<TimelineData> ();
            singaporeStats.Add (new TimelineData (196, new DateTime (2020, 4, 30), 30, 5));
            singaporeStats.Add (new TimelineData (196, new DateTime (2020, 4, 29), 50, 5));

            locations.Add (new Location (id: 1, country: "USA", lastUpdatedTime : new DateTime (2020, 4, 30), timelineData : usaStats));
            locations.Add (new Location (id: 2, country: "Singapore", lastUpdatedTime : new DateTime (2020, 4, 30), timelineData : singaporeStats));
            return locations;
        }

    }
}