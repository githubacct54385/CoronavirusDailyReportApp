using System;
using System.Collections.Generic;
using CoronavirusDailyReportApp.Core.Models;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CoronavirusDailyReportApp.Core.Requests {
    public sealed class CovidDataProviderImpl : ICovidDataProvider {
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

                    string country = GetCountry (covidData);
                    List<CovidStats> covidStats = GetCovidStatsForDatesOfInterest (covidData, reportInput.CompareDate);
                    Location loc = new Location (countryId, country, covidStats);
                    locations.Add (loc);
                }
            }
            return locations;
        }

        private List<CovidStats> GetCovidStatsForDatesOfInterest (JObject covidData, DateTime compareDate) {
            int deathsForToday = DeathsForDay (GetToday (), covidData);
            int deathsForOldDate = DeathsForDay (compareDate, covidData);
            int confirmedForToday = ConfirmedForDay (GetToday (), covidData);
            int confirmedForOldDate = ConfirmedForDay (compareDate, covidData);

            List<CovidStats> covidStats = new List<CovidStats> ();
            covidStats.Add (new CovidStats (confirmedForToday, deathsForToday, GetToday ()));
            covidStats.Add (new CovidStats (confirmedForOldDate, deathsForOldDate, compareDate));

            return covidStats;
        }

        private int DeathsForDay (DateTime date, JObject covidData) {
            string year = GetYear (date);
            string month = GetMonth (date);
            string day = GetDay (date);
            string dayAsString = GetDayAsString (year, month, day);
            JToken deaths = covidData.GetValue ("location") ["timelines"]["deaths"]["timeline"][dayAsString];
            return deaths.Value<int> ();
        }

        private int ConfirmedForDay (DateTime date, JObject covidData) {
            string year = GetYear (date);
            string month = GetMonth (date);
            string day = GetDay (date);
            string dayAsString = GetDayAsString (year, month, day);
            JToken deaths = covidData.GetValue ("location") ["timelines"]["confirmed"]["timeline"][dayAsString];
            return deaths.Value<int> ();
        }

        private string GetYear (DateTime date) => date.ToString ("yyyy");
        private string GetMonth (DateTime date) => date.ToString ("MM");
        private string GetDay (DateTime date) => date.ToString ("dd");
        private string GetDayAsString (string year, string month, string day) => $"{year}-{month}-{day}T00:00:00Z";

        private string GetCountry (JObject covidData) {
            JToken country = covidData.GetValue ("location") ["country"];
            return country.Value<string> ();
        }

        public DateTime GetToday () {
            // subtract a day since UTC is ahead of the data available
            return DateTime.Today.AddDays (-1);
        }
    }
}