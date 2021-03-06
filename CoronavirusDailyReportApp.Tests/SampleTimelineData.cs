namespace CoronavirusDailyReportApp.Tests {
    public static class SampleTimelineData {
        public static string SampleTimelineJson () {
            const string json = "{ \"location\": { \"id\": 225, \"country\": \"US\", \"country_code\": \"US\", \"country_population\": 327167434, \"province\": \"\", \"county\": \"\", \"last_updated\": \"2020-05-02T06:23:40.348976Z\", \"coordinates\": { \"latitude\": \"37.0902\", \"longitude\": \"-95.7129\" }, \"latest\": { \"confirmed\": 1103461, \"deaths\": 64943, \"recovered\": 0 }, \"timelines\": { \"confirmed\": { \"latest\": 1103461, \"timeline\": { \"2020-04-29T00:00:00Z\": 1039909, \"2020-04-30T00:00:00Z\": 1069424, \"2020-05-01T00:00:00Z\": 1103461 } }, \"deaths\": { \"latest\": 64943, \"timeline\": { \"2020-04-29T00:00:00Z\": 60967, \"2020-04-30T00:00:00Z\": 62996, \"2020-05-01T00:00:00Z\": 64943 } }, \"recovered\": { \"latest\": 0, \"timeline\": {} } } } } ";
            return json;
        }
    }
}