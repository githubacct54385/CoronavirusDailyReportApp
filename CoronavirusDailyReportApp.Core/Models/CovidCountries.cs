using System;
using System.Collections.Generic;

namespace CoronavirusDailyReportApp.Core.Models {
    public class CovidCountries {
        private List<int> _countryIds { get; set; }
        public void AddCountryId (int countryId) {
            if (_countryIds.Contains (countryId)) {
                throw new ArgumentException ($"CountryId of {countryId} already exists in list of CountryIds.");
            }
            _countryIds.Add (countryId);
        }

        public List<int> CountryIds => _countryIds;
    }
}