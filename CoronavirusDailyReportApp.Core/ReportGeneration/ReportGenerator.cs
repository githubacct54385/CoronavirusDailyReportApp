using System.Collections.Generic;
using CoronavirusDailyReportApp.Core.Models;
using CoronavirusDailyReportApp.Core.Requests;

namespace CoronavirusDailyReportApp.Core.ReportGeneration {
    public class ReportGenerator {
        private readonly ICovidDataProvider _covidDataProvider;
        private readonly IReportValuesProvider _reportValuesProvider;

        public ReportGenerator (ICovidDataProvider covidDataProvider, IReportValuesProvider reportValuesProvider) {
            _covidDataProvider = covidDataProvider;
            _reportValuesProvider = reportValuesProvider;
        }
        public ReportModel GenerateReport (ReportInput reportInput) {
            List<Location> locations = GetPayloads (reportInput);
            ReportModel reportModel = new ReportModel (locations, reportInput.CompareDate, _reportValuesProvider);
            return reportModel;
        }

        private List<Location> GetPayloads (ReportInput reportInput) {
            CovidRequester covidRequester = new CovidRequester (_covidDataProvider);
            List<Location> response = covidRequester.MakeRequest (reportInput);
            return response;
        }
    }
}