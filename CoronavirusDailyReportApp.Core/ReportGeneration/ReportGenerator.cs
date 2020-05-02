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
        public ReportModel GenerateReport (int[] countryCodes, CovidDates covidDates) {
            List<NewLocation> payloads = GetPayloads (countryCodes, covidDates);
            ReportModel reportModel = new ReportModel (payloads, covidDates.NewDate, _reportValuesProvider);
            return reportModel;
        }

        private List<NewLocation> GetPayloads (int[] countryCodes, CovidDates covidDates) {
            CovidRequester covidRequester = new CovidRequester (_covidDataProvider);
            List<NewLocation> response = covidRequester.MakeRequest (countryCodes, covidDates);
            return response;
        }
    }
}