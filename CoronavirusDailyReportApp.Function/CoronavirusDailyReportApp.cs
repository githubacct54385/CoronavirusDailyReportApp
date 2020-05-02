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

            CovidCountries covidCountries = new CovidCountries ();
            covidCountries.AddCountryId (225);
            covidCountries.AddCountryId (196);
            covidCountries.AddCountryId (132);
            covidCountries.AddCountryId (201);
            covidCountries.AddCountryId (120);
            covidCountries.AddCountryId (49);
            covidCountries.AddCountryId (137);

            // select the dates for comparison
            CovidDates covidDates = new CovidDates (DateTime.Today.AddDays (-2), DateTime.Today.AddDays (-3));

            ReportInput reportInput = new ReportInput (covidCountries.CountryIds, covidDates);
            // creates a report for posting to slack
            ReportGenerator reportGenerator = new ReportGenerator (new CovidDataProviderImpl (), new ReportValuesProviderImpl ());
            ReportModel reportModel = reportGenerator.GenerateReport (reportInput);

            // write to slack
            SlackWriter slackWriter = new SlackWriter (new RestProviderImpl ());
            slackWriter.Write (reportModel.SlackMessage);
        }
    }
}