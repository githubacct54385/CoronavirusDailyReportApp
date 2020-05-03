using System.Collections.Generic;
using CoronavirusDailyReportApp.Core.Models;
using CoronavirusDailyReportApp.Core.Requests;

namespace CoronavirusDailyReportApp.Core.ReportGeneration {
    public class ReportGenerator {
        private readonly ICovidDataProvider _covidDataProvider;

        public ReportGenerator (ICovidDataProvider covidDataProvider) {
            _covidDataProvider = covidDataProvider;
        }
        public ReportModel GenerateReport (ReportInput reportInput) {
            List<Location> locations = GetPayloads (reportInput);
            ReportModel reportModel = new ReportModel (locations);
            return reportModel;
        }

        private List<Location> GetPayloads (ReportInput reportInput) {
            CovidRequester covidRequester = new CovidRequester (_covidDataProvider);
            List<Location> response = covidRequester.MakeRequest (reportInput);
            return response;
        }
    }
}