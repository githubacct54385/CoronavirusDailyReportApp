using System.Collections.Generic;
using CoronavirusDailyReportApp.Core.Models;

namespace CoronavirusDailyReportApp.Core.Requests {
    public interface ICovidDataProvider {
        List<Location> GetCovidDataWithCompare (int[] countryIds, CovidDates covidDates);
    }
}