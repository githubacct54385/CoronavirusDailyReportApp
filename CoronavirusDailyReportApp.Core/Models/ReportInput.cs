using System;
using System.Collections.Generic;

namespace CoronavirusDailyReportApp.Core.Models {
    public class ReportInput {
        public ReportInput (List<int> countryIds) {
            CountryIds = countryIds;
        }

        public List<int> CountryIds { get; set; }
    }
}