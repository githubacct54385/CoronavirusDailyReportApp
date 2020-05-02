using System;
using System.Collections.Generic;
using CoronavirusDailyReportApp.Core.Models;
using CoronavirusDailyReportApp.Core.Utils;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CoronavirusDailyReportApp.Core.Requests {
    public sealed class CovidDataProviderImpl : ICovidDataProvider {
        private readonly ITimelineProvider _timelineProvider;
        public CovidDataProviderImpl (ITimelineProvider timelineProvider) {
            _timelineProvider = timelineProvider;
        }
        private const string apiSource = "jhu";
        private const string host = "https://coronavirus-tracker-api.herokuapp.com";

        public List<Location> GetCovidDataWithCompare (ReportInput reportInput) {
            var client = new RestClient (host);

            List<Location> locations = new List<Location> ();

            foreach (int countryId in reportInput.CountryIds) {
                var request = new RestRequest ($"/v2/locations/{countryId}?source={apiSource}&timelines=true");

                var response = client.Execute (request);
                if (response.IsSuccessful) {
                    string json = response.Content;
                    JObject covidData = JObject.Parse (json);

                    Timeline timeline = new Timeline (_timelineProvider);
                    List<TimelineData> timelineData = timeline.Create (json);

                    DateTime lastUpdatedDate = GetLastUpdatedate (covidData);

                    string country = GetCountry (covidData);
                    Location loc = new Location (countryId, country, lastUpdatedDate, timelineData);
                    locations.Add (loc);
                }
            }
            return locations;
        }

        private DateTime GetLastUpdatedate (JObject covidData) {
            JToken lastUpdateDate = covidData.GetValue ("location") ["last_updated"];
            DateTime date = lastUpdateDate.Value<DateTime> ();
            return date;
        }

        private string GetCountry (JObject covidData) {
            JToken country = covidData.GetValue ("location") ["country"];
            return country.Value<string> ();
        }
    }
}