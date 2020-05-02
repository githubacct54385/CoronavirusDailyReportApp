using System;
using CoronavirusDailyReportApp.Core.Models;
using CoronavirusDailyReportApp.Core.ReportGeneration;
using CoronavirusDailyReportApp.Core.Requests;
using CoronavirusDailyReportApp.Core.Slack;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CoronavirusDailyReportApp {
    public static class CoronavirusDailyReportApp {
        [FunctionName ("CoronavirusDailyReportApp")]
        public static void Run ([TimerTrigger ("*/30 * * * * *")] TimerInfo myTimer, ILogger log) {
            // Country Codes from Coronavirus API
            // 225 = US
            // 196 = Singapore
            // 132 = Indonesia
            int[] countryCodes = new int[] { 225, 196, 132, 201, 120, 49, 137 };

            // select the dates for comparison
            CovidDates covidDates = new CovidDates (DateTime.Today.AddDays (-2), DateTime.Today.AddDays (-3));

            // creates a report for posting to slack
            ReportGenerator reportGenerator = new ReportGenerator (new CovidDataProviderImpl (), new ReportValuesProviderImpl ());
            ReportModel reportModel = reportGenerator.GenerateReport (countryCodes, covidDates);

            // write to slack
            SlackWriter slackWriter = new SlackWriter (new RestProviderImpl ());
            slackWriter.Write (reportModel.SlackMessage);
        }
    }
}