using System;
using System.Collections.Generic;
using System.Text.Json;
using CoronavirusDailyReportApp.Core.Models;
using CoronavirusDailyReportApp.Core.ReportGeneration;
using CoronavirusDailyReportApp.Core.Requests;
using CoronavirusDailyReportApp.Core.Slack;
using CoronavirusDailyReportApp.Core.Utils;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace CoronavirusDailyReportApp {
    public static class CoronavirusDailyReportApp {
        [FunctionName ("CoronavirusDailyReportApp")]
        public static void Run ([TimerTrigger ("0 0 12 * * *")] TimerInfo myTimer, ILogger log) {
            CovidCountries covidCountries = GetCovidCountries ();

            ReportInput reportInput = new ReportInput (covidCountries.CountryIds);
            // creates a report for posting to slack
            ReportGenerator reportGenerator = new ReportGenerator (new CovidDataProviderImpl (new TimelineProviderImpl ()), new ReportValuesProviderImpl ());
            ReportModel reportModel = reportGenerator.GenerateReport (reportInput);

            // write to slack
            SlackWriter slackWriter = new SlackWriter (new RestProviderImpl ());
            slackWriter.Write (reportModel.SlackMessage);
        }

        private static CovidCountries GetCovidCountries () {
            string countriesCsv = System.Environment.GetEnvironmentVariable ("CovidCountries");

            CovidCountries covidCountries = CovidCountriesUtils.ParseCsv (countriesCsv);

            return covidCountries;
        }
    }
}