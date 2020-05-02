using System;

namespace CoronavirusDailyReportApp.Core.Models {
    public sealed class ReportValuesProviderImpl : IReportValuesProvider {
        public string GetReportTime () {
            return DateTime.UtcNow.AddHours (8).ToString ("dddd MMMM dd, yyyy hh:mm:00 tt");
        }
    }
}