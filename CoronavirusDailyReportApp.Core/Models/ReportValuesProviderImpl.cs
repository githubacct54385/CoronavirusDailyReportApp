using System;

namespace CoronavirusDailyReportApp.Core.Models {
    public sealed class ReportValuesProviderImpl : IReportValuesProvider {
        public string GetReportTime () {
            return DateTime.Now.ToString ("dddd MMMM dd, yyyy hh:mm:00 tt");
        }
    }
}