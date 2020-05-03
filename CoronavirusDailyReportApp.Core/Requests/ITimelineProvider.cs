using System.Collections.Generic;
using CoronavirusDailyReportApp.Core.Models;

namespace CoronavirusDailyReportApp.Core.Requests {
    public interface ITimelineProvider {
        List<TimelineData> TimelineData (string json);
    }
}