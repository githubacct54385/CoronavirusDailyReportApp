namespace CoronavirusDailyReportApp.Core.Config {
    public interface ISlackConfig {
        string GetWebhookSecret ();
    }
}