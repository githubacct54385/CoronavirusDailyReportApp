using System.Collections.Generic;

namespace CoronavirusDailyReportApp.Core.Models {
    public class ReportInput {
        public ReportInput (List<int> countryIds, CovidDates covidDates) {
            CountryIds = countryIds;
            CovidDates = covidDates;
        }

        public List<int> CountryIds { get; set; }
        public CovidDates CovidDates { get; set; }
    }
}