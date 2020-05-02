using System;
using System.Collections.Generic;

namespace CoronavirusDailyReportApp.Core.Models {
    public class ReportInput {
        public ReportInput (List<int> countryIds, DateTime compareDate) {
            CountryIds = countryIds;
            CompareDate = compareDate;
        }

        public List<int> CountryIds { get; set; }
        public DateTime CompareDate { get; set; }
    }
}