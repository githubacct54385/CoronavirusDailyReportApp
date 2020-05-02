namespace CoronavirusDailyReportApp.Core.Config {
    public class SlackConfigImpl : ISlackConfig {
        public string GetWebhookSecret () {
            // Notes:
            // https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-class-library#environment-variables
            // How to use environment variables
            // make sure you put the WebhookSecret in your local.settings.json file too

            string webhookSecret = System.Environment.GetEnvironmentVariable ("WebhookSecret");
            return webhookSecret;
        }
    }
}