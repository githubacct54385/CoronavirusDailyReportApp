using System;
using Autofac.Extras.Moq;
using CoronavirusDailyReportApp.Core.Models;
using CoronavirusDailyReportApp.Core.ReportGeneration;
using CoronavirusDailyReportApp.Core.Requests;
using CoronavirusDailyReportApp.Core.Slack;
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

            string expectedSlackMessage = $"Covid Cases For Thursday April 30, 2020 00:09:05 AM\n\nUSA\n55,000 Deaths (+10,000)\n1,000,000 Confirmed (+50,000)\n\nSingapore\n5 Deaths (+2)\n50 Confirmed (+20)\n\n";

            var locations = SampleTestData.SampleLocations ();

            CovidCountries covidCountries = new CovidCountries ();
            covidCountries.AddCountryId (1);
            covidCountries.AddCountryId (2);

            ReportInput reportInput = new ReportInput (covidCountries.CountryIds, new DateTime (2020, 4, 29));

            using (AutoMock mock = AutoMock.GetLoose ()) {

                mock.Mock<ICovidDataProvider> ()
                    .Setup (x => x.GetCovidDataWithCompare (reportInput))
                    .Returns (locations);

                mock.Mock<ICovidDataProvider> ()
                    .Setup (x => x.GetToday ())
                    .Returns (new DateTime (2020, 4, 30));

                mock.Mock<IReportValuesProvider> ()
                    .Setup (x => x.GetReportTime ())
                    .Returns ("Thursday April 30, 2020 00:09:05 AM");

                ReportGenerator sut = mock.Create<ReportGenerator> ();

                var actual = sut.GenerateReport (reportInput);

                Assert.Equal (expectedSlackMessage, actual.SlackMessage);
            }
        }

        [Fact]
        public void ReportModel_ShowsNegativeChangeWhenPreviousDayHasLessCases () {
            string expectedSlackMessage = $"Covid Cases For Friday May 01, 2020 00:10:55 AM\n\nUSA\n75,000 Deaths (No Change)\n175,000 Confirmed (-25,000)\n\nSingapore\n5 Deaths (No Change)\n30 Confirmed (-20)\n\n";

            var locations = SampleTestData.SampleNegativeChangeLocations ();

            CovidCountries covidCountries = new CovidCountries ();
            covidCountries.AddCountryId (1);
            covidCountries.AddCountryId (2);

            ReportInput reportInput = new ReportInput (covidCountries.CountryIds, new DateTime (2020, 4, 29));

            using (AutoMock mock = AutoMock.GetLoose ()) {

                mock.Mock<ICovidDataProvider> ()
                    .Setup (x => x.GetCovidDataWithCompare (reportInput))
                    .Returns (locations);

                mock.Mock<ICovidDataProvider> ()
                    .Setup (x => x.GetToday ())
                    .Returns (new DateTime (2020, 4, 30));

                mock.Mock<IReportValuesProvider> ()
                    .Setup (x => x.GetReportTime ())
                    .Returns ("Friday May 01, 2020 00:10:55 AM");

                ReportGenerator sut = mock.Create<ReportGenerator> ();

                var actual = sut.GenerateReport (reportInput);

                Assert.Equal (expectedSlackMessage, actual.SlackMessage);
            }
        }
    }
}