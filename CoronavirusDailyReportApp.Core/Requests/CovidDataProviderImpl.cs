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
                    //DateTime compareDate = lastUpdatedDate.Date.AddDays (-1);

                    string country = GetCountry (covidData);
                    //List<CovidStats> covidStats = GetCovidStatsForDatesOfInterest (covidData, compareDate);
                    Location loc = new Location (countryId, country, lastUpdatedDate, timelineData);
                    locations.Add (loc);
                }
            }
            return locations;
        }

        private List<CovidStats> GetCovidStatsForDatesOfInterest (JObject covidData, DateTime compareDate) {
            int latestDeaths = LatestDeaths (covidData);
            int deathsForOldDate = DeathsForDay (compareDate, covidData);
            int latestConfirmed = LatestConfirmed (covidData);
            int confirmedForOldDate = ConfirmedForDay (compareDate, covidData);

            DateTime lastUpdatedDate = GetLastUpdatedate (covidData);

            List<CovidStats> covidStats = new List<CovidStats> ();
            covidStats.Add (new CovidStats (latestConfirmed, latestDeaths, lastUpdatedDate.Date, true));
            covidStats.Add (new CovidStats (confirmedForOldDate, deathsForOldDate, compareDate, false));

            return covidStats;
        }

        private DateTime GetLastUpdatedate (JObject covidData) {
            JToken lastUpdateDate = covidData.GetValue ("location") ["last_updated"];
            DateTime date = lastUpdateDate.Value<DateTime> ();
            return date;
        }

        private int LatestDeaths (JObject covidData) {
            JToken deaths = covidData.GetValue ("location") ["latest"]["deaths"];
            return deaths.Value<int> ();
        }

        private int LatestConfirmed (JObject covidData) {
            JToken confirmed = covidData.GetValue ("location") ["latest"]["confirmed"];
            return confirmed.Value<int> ();
        }

        private int DeathsForDay (DateTime date, JObject covidData) {
            string dayAsString = DateUtils.GetDateToken (date);
            JToken deaths = covidData.GetValue ("location") ["timelines"]["deaths"]["timeline"][dayAsString];
            return deaths.Value<int> ();
        }

        private int ConfirmedForDay (DateTime date, JObject covidData) {
            string dayAsString = DateUtils.GetDateToken (date);
            JToken deaths = covidData.GetValue ("location") ["timelines"]["confirmed"]["timeline"][dayAsString];
            return deaths.Value<int> ();
        }

        private string GetCountry (JObject covidData) {
            JToken country = covidData.GetValue ("location") ["country"];
            return country.Value<string> ();
        }
    }
}