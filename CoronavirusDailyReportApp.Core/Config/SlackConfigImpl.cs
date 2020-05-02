using System.IO;

namespace CoronavirusDailyReportApp.Core.Config {
    public class SlackConfigImpl : ISlackConfig {
        private const string PATH = "/Users/alex/Documents/C#/CoronavirusDailyReportApp/CoronavirusDailyReportApp.Core/Config/Secret.txt";
        public string GetWebhookSecret () {
            string secret = File.ReadAllText (PATH);
            return secret;
        }
    }
}