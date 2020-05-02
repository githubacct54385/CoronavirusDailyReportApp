using CoronavirusDailyReportApp.Core.Config;
using CoronavirusDailyReportApp.Core.Models;
using RestSharp;

namespace CoronavirusDailyReportApp.Core.Slack {
    public sealed class RestProviderImpl : IRestProvider {
        private readonly ISlackConfig _provider;
        public RestProviderImpl () {
            _provider = new SlackConfigImpl ();
        }
        public RestProviderImpl (ISlackConfig provider) {
            _provider = provider;
        }
        public SlackWriteResponse SendMessage (string message) {
            const string host = "https://hooks.slack.com";
            string webhookSecret = _provider.GetWebhookSecret ();
            string path = $"services/{webhookSecret}";

            var client = new RestClient (host);

            var request = new RestRequest (path, Method.POST, DataFormat.Json);
            request.AddJsonBody (new { text = message });

            var response = client.Execute (request);

            return new SlackWriteResponse () { Sent = true, Message = message };
        }
    }
}