using System;
using System.Collections.Generic;
using CoronavirusDailyReportApp.Core.Models;

namespace CoronavirusAzFunction.Tests {
    public static class SampleTestData {
        public static List<NewLocation> SampleLocations () {

            List<NewLocation> locations = new List<NewLocation> ();

            List<CovidStats> usaStats = new List<CovidStats> ();
            usaStats.Add (new CovidStats (100, 50, new DateTime (2020, 4, 30)));
            usaStats.Add (new CovidStats (75, 30, new DateTime (2020, 4, 29)));

            List<CovidStats> singaporeStats = new List<CovidStats> ();
            singaporeStats.Add (new CovidStats (50, 5, new DateTime (2020, 4, 30)));
            singaporeStats.Add (new CovidStats (30, 3, new DateTime (2020, 4, 29)));

            locations.Add (new NewLocation (id: 1, country: "USA", dailyStats : usaStats));
            locations.Add (new NewLocation (id: 2, country: "Singapore", dailyStats : singaporeStats));
            return locations;
        }

        public static List<NewLocation> SampleNegativeChangeLocations () {

            List<NewLocation> locations = new List<NewLocation> ();

            List<CovidStats> usaStats = new List<CovidStats> ();
            usaStats.Add (new CovidStats (75, 50, new DateTime (2020, 4, 30)));
            usaStats.Add (new CovidStats (100, 50, new DateTime (2020, 4, 29)));

            List<CovidStats> singaporeStats = new List<CovidStats> ();
            singaporeStats.Add (new CovidStats (30, 5, new DateTime (2020, 4, 30)));
            singaporeStats.Add (new CovidStats (50, 5, new DateTime (2020, 4, 29)));

            locations.Add (new NewLocation (id: 1, country: "USA", dailyStats : usaStats));
            locations.Add (new NewLocation (id: 2, country: "Singapore", dailyStats : singaporeStats));
            return locations;
        }

    }
}