using System.IO;

namespace CoronavirusDailyReportApp.Tests {
    public static class SampleTimelineData {
        public static string SampleTimelineJson () {
            string path = "/Users/alex/Documents/C#/CoronavirusDailyReportApp/CoronavirusDailyReportApp.Tests/sampleTimeline.json";
            return File.ReadAllText (path);
        }
    }
}