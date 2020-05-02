using System;
using CoronavirusDailyReportApp.Core.Models;

namespace CoronavirusDailyReportApp.Core.Utils {
    public class CovidCountriesUtils {
        public static CovidCountries ParseCsv (string csv) {
            string[] countries = csv.Trim ().Split (',');

            CovidCountries covidCountries = new CovidCountries ();
            foreach (string country in countries) {
                covidCountries.AddCountryId (int.Parse (country));
            }
            return covidCountries;
        }
    }
}