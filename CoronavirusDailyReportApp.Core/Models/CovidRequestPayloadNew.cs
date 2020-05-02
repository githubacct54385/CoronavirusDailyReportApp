using System.Collections.Generic;

namespace CoronavirusDailyReportApp.Core.Models {
    public class CovidRequestPayloadNew {
        public CovidRequestPayloadNew (List<Location> locations) {
            Locations = locations;
        }

        public List<Location> Locations { get; set; }

    }
}