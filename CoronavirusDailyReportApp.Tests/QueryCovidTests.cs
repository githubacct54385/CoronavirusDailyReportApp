using System;
using System.Collections.Generic;
using Autofac.Extras.Moq;
using CoronavirusAzFunction.Tests;
using CoronavirusDailyReportApp.Core.Models;
using CoronavirusDailyReportApp.Core.Requests;
using Xunit;

namespace CoronavirusReportApp.Tests {
    public class QueryCovidTests {
        [Fact]
        public void QueryCovid_ReturnsCorrectResults () {

            List<Location> expected = SampleTestData.SampleLocations ();

            CovidCountries covidCountries = new CovidCountries ();
            covidCountries.AddCountryId (1);
            covidCountries.AddCountryId (2);

            ReportInput reportInput = new ReportInput (covidCountries.CountryIds, new DateTime (2020, 4, 29));

            using (AutoMock mock = AutoMock.GetLoose ()) {
                mock.Mock<ICovidDataProvider> ()
                    .Setup (x => x.GetCovidDataWithCompare (reportInput))
                    .Returns (expected);

                CovidRequester sut = mock.Create<CovidRequester> ();
                List<Location> actual = sut.MakeRequest (reportInput);

                for (int i = 0; i < expected.Count; i++) {
                    Assert.Equal (expected[i].Id, actual[i].Id);
                    Assert.Equal (expected[i].Country, actual[i].Country);
                    Assert.Equal (expected[i].TimelineData[i].TimelineDate, actual[i].TimelineData[i].TimelineDate);
                    Assert.Equal (expected[i].TimelineData[i].Confirmed, actual[i].TimelineData[i].Confirmed);
                    Assert.Equal (expected[i].TimelineData[i].Deaths, actual[i].TimelineData[i].Deaths);
                }
            }
        }
    }
}