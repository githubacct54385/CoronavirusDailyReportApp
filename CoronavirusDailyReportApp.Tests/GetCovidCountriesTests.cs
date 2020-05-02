using CoronavirusDailyReportApp.Core.Models;
using CoronavirusDailyReportApp.Core.Utils;
using Xunit;

namespace CoronavirusDailyReportApp.Tests {
    public class GetCovidCountriesTests {
        [Fact]
        public void GetCovidCountriesFromCsv_ReturnsCorrectArrayOfString () {

            CovidCountries expected = new CovidCountries ();
            expected.AddCountryId (1);
            expected.AddCountryId (2);
            expected.AddCountryId (3);
            expected.AddCountryId (4);
            expected.AddCountryId (5);

            string csv = "1,2,3,4,5";
            CovidCountries actual = CovidCountriesUtils.ParseCsv (csv);

            for (int i = 0; i < expected.CountryIds.Count; i++) {
                Assert.Equal (expected.CountryIds[i], actual.CountryIds[i]);
            }
        }
    }
}