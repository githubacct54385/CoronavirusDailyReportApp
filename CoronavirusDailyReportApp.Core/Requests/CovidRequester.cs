using System.Collections.Generic;
using CoronavirusDailyReportApp.Core.Models;

namespace CoronavirusDailyReportApp.Core.Requests {
    public class CovidRequester {
        private readonly ICovidDataProvider _provider;

        public CovidRequester (ICovidDataProvider provider) {
            _provider = provider;
        }
        public List<Location> MakeRequest (ReportInput reportInput) {
            return _provider.GetCovidDataWithCompare (reportInput);
        }
    }
}