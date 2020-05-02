using System;
using System.Collections.Generic;
using System.Text.Json;
using CoronavirusDailyReportApp.Core.Models;
using CoronavirusDailyReportApp.Core.ReportGeneration;
using CoronavirusDailyReportApp.Core.Requests;
using CoronavirusDailyReportApp.Core.Slack;
using CoronavirusDailyReportApp.Core.Utils;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CoronavirusDailyReportApp {
    public static class CoronavirusDailyReportApp {
        [FunctionName ("CoronavirusDailyReportApp")]
        public static void Run ([TimerTrigger ("0 0 0 * * *")] TimerInfo myTimer, ILogger log) {
            CovidCountries covidCountries = GetCovidCountries ();

            // select the dates for comparison
            CovidDates covidDates = GetCovidDates ();

            ReportInput reportInput = new ReportInput (covidCountries.CountryIds, covidDates);
            // creates a report for posting to slack
            ReportGenerator reportGenerator = new ReportGenerator (new CovidDataProviderImpl (), new ReportValuesProviderImpl ());
            ReportModel reportModel = reportGenerator.GenerateReport (reportInput);

            // write to slack
            SlackWriter slackWriter = new SlackWriter (new RestProviderImpl ());
            slackWriter.Write (reportModel.SlackMessage);
        }

        public class Country {
            public int Id { get; set; }
        }

        private static CovidCountries GetCovidCountries () {
            string countriesCsv = System.Environment.GetEnvironmentVariable ("CovidCountries");

            CovidCountries covidCountries = CovidCountriesUtils.ParseCsv (countriesCsv);

            return covidCountries;
        }

        // Reads environment vars for new and old dates
        // converts them for the new and old date for covid tracking
        private static CovidDates GetCovidDates () {
            string newCovidDateMinusDaysAsString = System.Environment.GetEnvironmentVariable ("NewCovidDateMinusDays");
            string oldCovidDateMinusDaysAsString = System.Environment.GetEnvironmentVariable ("OldCovidDateMinusDays");

            int newCovidDateMinusDays = int.Parse (newCovidDateMinusDaysAsString);
            int oldCovidDateMinusDays = int.Parse (oldCovidDateMinusDaysAsString);

            return new CovidDates (DateTime.Today.AddDays (newCovidDateMinusDays), DateTime.Today.AddDays (oldCovidDateMinusDays));
        }
    }
}