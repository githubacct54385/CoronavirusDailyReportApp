using System;

namespace CoronavirusDailyReportApp.Core.Models {
    public sealed class ReportValuesProviderImpl : IReportValuesProvider {
        public string GetReportTime () {
            return DateTime.Today.ToString ("dddd MMMM dd, yyyy");
        }
    }
}