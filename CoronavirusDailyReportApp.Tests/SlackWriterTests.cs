using System;
using Autofac.Extras.Moq;
using CoronavirusDailyReportApp.Core.Models;
using CoronavirusDailyReportApp.Core.ReportGeneration;
using CoronavirusDailyReportApp.Core.Requests;
using CoronavirusDailyReportApp.Core.Slack;
using CoronavirusDailyReportApp.Tests;
using Xunit;

namespace CoronavirusAzFunction.Tests {
    public class SlackWriterTests {
        [Fact]
        public void Write_RunsOnceAsExpected () {
            string message = "Report for date";

            using (AutoMock mock = AutoMock.GetLoose ()) {

                mock.Mock<IRestProvider> ()
                    .Setup (x => x.SendMessage (message))
                    .Returns (new SlackWriteResponse () { Sent = true, Message = message });

                SlackWriter sut = mock.Create<SlackWriter> ();
                SlackWriteResponse actual = sut.Write (message);

                Assert.True (actual.Sent);
                Assert.Equal (message, actual.Message);

            }
        }

        [Fact]
        public void ReportModel_CreatesCorrectSlackMessage () {

            string expectedSlackMessage = $"Covid Cases For Thursday April 30, 2020\nComparing with Wednesday April 29, 2020\n\nUSA\n1,000,000 Deaths (+50,000)\n55,000 Confirmed (+10,000)\n\nSingapore\n50 Deaths (+20)\n5 Confirmed (+2)\n\n";

            var locations = SampleTestData.SampleLocations ();
            var json = SampleTimelineData.SampleTimelineJson ();

            CovidCountries covidCountries = new CovidCountries ();
            covidCountries.AddCountryId (1);
            covidCountries.AddCountryId (2);

            ReportInput reportInput = new ReportInput (covidCountries.CountryIds);

            using (AutoMock mock = AutoMock.GetLoose ()) {

                mock.Mock<ICovidDataProvider> ()
                    .Setup (x => x.GetCovidDataWithCompare (reportInput))
                    .Returns (locations);

                ReportGenerator sut = mock.Create<ReportGenerator> ();

                var actual = sut.GenerateReport (reportInput);

                Assert.Equal (expectedSlackMessage, actual.SlackMessage);
            }
        }

        [Fact]
        public void ReportModel_ShowsNegativeChangeWhenPreviousDayHasLessCases () {
            string expectedSlackMessage = $"Covid Cases For Thursday April 30, 2020\nComparing with Wednesday April 29, 2020\n\nUSA\n175,000 Deaths (-25,000)\n75,000 Confirmed (No Change)\n\nSingapore\n30 Deaths (-20)\n5 Confirmed (No Change)\n\n";

            var locations = SampleTestData.SampleNegativeChangeLocations ();

            CovidCountries covidCountries = new CovidCountries ();
            covidCountries.AddCountryId (1);
            covidCountries.AddCountryId (2);

            ReportInput reportInput = new ReportInput (covidCountries.CountryIds);

            using (AutoMock mock = AutoMock.GetLoose ()) {

                mock.Mock<ICovidDataProvider> ()
                    .Setup (x => x.GetCovidDataWithCompare (reportInput))
                    .Returns (locations);

                ReportGenerator sut = mock.Create<ReportGenerator> ();

                var actual = sut.GenerateReport (reportInput);

                Assert.Equal (expectedSlackMessage, actual.SlackMessage);
            }
        }
    }
}