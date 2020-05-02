using CoronavirusDailyReportApp.Core.Models;

namespace CoronavirusDailyReportApp.Core.Slack {
    public interface IRestProvider {
        SlackWriteResponse SendMessage (string message);
    }
}