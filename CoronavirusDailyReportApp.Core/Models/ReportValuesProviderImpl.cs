using System;
using CustomDateTimeExtension.Core;

namespace CoronavirusDailyReportApp.Core.Models {
    public sealed class ReportValuesProviderImpl : IReportValuesProvider {
        public string GetReportTime () {
            return DateTime.Today.CustomToString ();
        }
    }
}