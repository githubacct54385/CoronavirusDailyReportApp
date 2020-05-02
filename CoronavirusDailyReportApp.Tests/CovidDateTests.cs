using System;
using CoronavirusDailyReportApp.Core.Models;
using Xunit;

namespace CoronavirusAzFunction.Tests {
    public class CovidDateTests {
        [Fact]
        public void CovidDate_Constructor_ThrowsArgExWhenTodayIsLessThanCompareDate () {
            Assert.Throws<ArgumentException> (() => new CovidDates (new DateTime (2020, 1, 1), new DateTime (2020, 2, 1)));
        }
    }
}