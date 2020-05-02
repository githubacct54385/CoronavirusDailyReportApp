using System;
using System.Collections.Generic;
using CoronavirusDailyReportApp.Core.Models;

namespace CoronavirusDailyReportApp.Core.Requests {
    public class Timeline {
        private readonly ITimelineProvider _provider;
        public Timeline (ITimelineProvider provider) {
            _provider = provider;

        }
        public List<TimelineData> Create (string json) {
            return _provider.TimelineData (json);
        }
    }
}