using CoronavirusDailyReportApp.Core.Models;

namespace CoronavirusDailyReportApp.Core.Slack {
    public class SlackWriter {
        private readonly IRestProvider _provider;

        public SlackWriter (IRestProvider provider) {
            _provider = provider;
        }
        public SlackWriteResponse Write (string message) {
            return _provider.SendMessage (message);
        }
    }
}