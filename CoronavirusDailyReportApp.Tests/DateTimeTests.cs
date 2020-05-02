using System;
using CoronavirusDailyReportApp.Core.DateLocalization;
using Xunit;

namespace CoronavirusAzFunction.Tests {
    public class DateTimeTests {
        [Fact]
        public void DisplayLocalizedDate_PerformsCorrectly () {

            DateTime expected = new DateTime (2020, 4, 1, 8, 0, 0, DateTimeKind.Utc);

            DateTime inputDateUtc = new DateTime (2020, 4, 1, 0, 0, 0, DateTimeKind.Utc);
            int hours = 8;

            DateLocalizer dateLocalizer = new DateLocalizer ();
            DateTime actual = dateLocalizer.LocalizeDate (inputDateUtc, hours);

            Assert.Equal (expected, actual);
        }

        [Fact]
        public void DisplayLocalizedDate_DisplaysCorrectToString () {
            const string pattern = "dddd MMMM dd, yyyy hh:mm:ss tt";
            DateTime expectedDate = new DateTime (2020, 4, 1, 8, 0, 0, DateTimeKind.Utc);
            string expectedString = "Wednesday April 01, 2020 08:00:00 AM SGT";

            DateTime inputDateUtc = new DateTime (2020, 4, 1, 0, 0, 0, DateTimeKind.Utc);
            int hours = 8;

            DateLocalizer dateLocalizer = new DateLocalizer ();
            string actualString = dateLocalizer.LocalizeDateToString (inputDateUtc, hours, pattern);

            Assert.Equal (expectedString, actualString);
        }
    }
}