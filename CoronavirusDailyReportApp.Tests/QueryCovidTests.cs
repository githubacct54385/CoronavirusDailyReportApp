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

                // mock.Mock<ICovidDataProvider> ()
                //     .Setup (x => x.GetToday ())
                //     .Returns (new DateTime (2020, 4, 30));

                CovidRequester sut = mock.Create<CovidRequester> ();
                List<Location> actual = sut.MakeRequest (reportInput);

                for (int i = 0; i < expected.Count; i++) {
                    Assert.Equal (expected[i].Id, actual[i].Id);
                    Assert.Equal (expected[i].Country, actual[i].Country);
                    Assert.Equal (expected[i].TimelineData[i].TimelineDate, actual[i].TimelineData[i].TimelineDate);
                    Assert.Equal (expected[i].TimelineData[i].Confirmed, actual[i].TimelineData[i].Confirmed);
                    Assert.Equal (expected[i].TimelineData[i].Deaths, actual[i].TimelineData[i].Deaths);
                }

                // Assert.Equal (expected[0].Id, actual[0].Id);
                // Assert.Equal (expected[1].Id, actual[1].Id);

                // Assert.Equal (expected[0].Country, actual[0].Country);
                // Assert.Equal (expected[1].Country, actual[1].Country);

                // // Check USA Stats
                // Assert.Equal (expected[0].TimelineData[0].TimelineDate, actual[0].time[0].Day);
                // Assert.Equal (expected[0].DailyStats[1].Day, actual[0].DailyStats[1].Day);

                // Assert.Equal (expected[0].DailyStats[0].Confirmed, actual[0].DailyStats[0].Confirmed);
                // Assert.Equal (expected[0].DailyStats[1].Confirmed, actual[0].DailyStats[1].Confirmed);

                // Assert.Equal (expected[0].DailyStats[0].Deaths, actual[0].DailyStats[0].Deaths);
                // Assert.Equal (expected[0].DailyStats[1].Deaths, actual[0].DailyStats[1].Deaths);

                // // Check Singapore Stats
                // Assert.Equal (expected[1].DailyStats[0].Day, actual[0].DailyStats[0].Day);
                // Assert.Equal (expected[1].DailyStats[1].Day, actual[1].DailyStats[1].Day);

                // Assert.Equal (expected[1].DailyStats[0].Confirmed, actual[1].DailyStats[0].Confirmed);
                // Assert.Equal (expected[1].DailyStats[1].Confirmed, actual[1].DailyStats[1].Confirmed);

                // Assert.Equal (expected[1].DailyStats[0].Deaths, actual[1].DailyStats[0].Deaths);
                // Assert.Equal (expected[1].DailyStats[1].Deaths, actual[1].DailyStats[1].Deaths);
            }
        }
    }
}