using System;
using System.Collections.Generic;
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

            List<int> covidCountries = new List<int> ();
            covidCountries.Add (225);
            covidCountries.Add (196);
            covidCountries.Add (132);
            covidCountries.Add (201);
            covidCountries.Add (120);
            covidCountries.Add (49);
            covidCountries.Add (137);

            // select the dates for comparison
            CovidDates covidDates = new CovidDates (DateTime.Today.AddDays (-2), DateTime.Today.AddDays (-3));

            ReportInput reportInput = new ReportInput (covidCountries, covidDates);
            // creates a report for posting to slack
            ReportGenerator reportGenerator = new ReportGenerator (new CovidDataProviderImpl (), new ReportValuesProviderImpl ());
            ReportModel reportModel = reportGenerator.GenerateReport (reportInput);

            // write to slack
            SlackWriter slackWriter = new SlackWriter (new RestProviderImpl ());
            slackWriter.Write (reportModel.SlackMessage);
        }
    }
}