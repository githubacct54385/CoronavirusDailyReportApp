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

            string expectedSlackMessage = $"Covid Cases For Thursday April 30, 2020 00:09:05 AM\n\nUSA\n50 Deaths (+20)\n100 Confirmed (+25)\n\nSingapore\n5 Deaths (+2)\n50 Confirmed (+20)\n\n";

            var locations = SampleTestData.SampleLocations ();

            CovidDates covidDates = new CovidDates (new DateTime (2020, 4, 30), new DateTime (2020, 4, 29));
            var countryCodes = new int[] { 1, 2 };

            using (AutoMock mock = AutoMock.GetLoose ()) {

                mock.Mock<ICovidDataProvider> ()
                    .Setup (x => x.GetCovidDataWithCompare (countryCodes, covidDates))
                    .Returns (locations);

                mock.Mock<IReportValuesProvider> ()
                    .Setup (x => x.GetReportTime ())
                    .Returns ("Thursday April 30, 2020 00:09:05 AM");

                ReportGenerator sut = mock.Create<ReportGenerator> ();

                var actual = sut.GenerateReport (countryCodes, covidDates);

                Assert.Equal (new DateTime (2020, 4, 30), actual.ReportDate);
                Assert.Equal (expectedSlackMessage, actual.SlackMessage);
                Assert.Equal ("Covid Cases For Thursday April 30, 2020 00:09:05 AM", actual.Header);
            }
        }

        [Fact]
        public void ReportModel_ShowsNegativeChangeWhenPreviousDayHasLessCases () {
            string expectedSlackMessage = $"Covid Cases For Friday May 01, 2020 00:10:55 AM\n\nUSA\n50 Deaths (No Change)\n75 Confirmed (-25)\n\nSingapore\n5 Deaths (No Change)\n30 Confirmed (-20)\n\n";

            var locations = SampleTestData.SampleNegativeChangeLocations ();

            CovidDates covidDates = new CovidDates (new DateTime (2020, 4, 30), new DateTime (2020, 4, 29));
            var countryCodes = new int[] { 1, 2 };

            using (AutoMock mock = AutoMock.GetLoose ()) {

                mock.Mock<ICovidDataProvider> ()
                    .Setup (x => x.GetCovidDataWithCompare (countryCodes, covidDates))
                    .Returns (locations);

                mock.Mock<IReportValuesProvider> ()
                    .Setup (x => x.GetReportTime ())
                    .Returns ("Friday May 01, 2020 00:10:55 AM");

                ReportGenerator sut = mock.Create<ReportGenerator> ();

                var actual = sut.GenerateReport (countryCodes, covidDates);

                Assert.Equal (new DateTime (2020, 4, 30), actual.ReportDate);
                Assert.Equal (expectedSlackMessage, actual.SlackMessage);
                Assert.Equal ("Covid Cases For Friday May 01, 2020 00:10:55 AM", actual.Header);
            }
        }
    }
}