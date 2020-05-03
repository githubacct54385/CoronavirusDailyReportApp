using System;
using System.Collections.Generic;
using CoronavirusDailyReportApp.Core.Models;
using CoronavirusDailyReportApp.Core.Utils;
using Newtonsoft.Json.Linq;

namespace CoronavirusDailyReportApp.Core.Requests {
    public sealed class TimelineProviderImpl : ITimelineProvider {
        public List<TimelineData> TimelineData (string json) {

            List<TimelineData> output = new List<TimelineData> ();

            JObject covidData = JObject.Parse (json);

            int countryId = covidData.GetValue ("location") ["id"].Value<int> ();

            JToken confirmedTimelines = covidData.GetValue ("location") ["timelines"]["confirmed"]["timeline"];
            JToken deathTimelines = covidData.GetValue ("location") ["timelines"]["deaths"]["timeline"];

            // the starting date available for the API is January 22nd, 2020
            DateTime startingDate = new DateTime (2020, 1, 22);

            while (startingDate < DateTime.Today) {
                string dateKey = DateUtils.GetDateToken (startingDate);

                JToken confirmedTokenForDay = confirmedTimelines[dateKey];
                JToken deathTokenForDay = deathTimelines[dateKey];

                if (confirmedTokenForDay == null && deathTokenForDay == null) {
                    // if the json does not have a value for confirmed and deaths
                    // break out of the while loop
                    break;
                }

                int confirmedForDay = confirmedTimelines[dateKey].Value<int> ();
                int deathsForDay = deathTimelines[dateKey].Value<int> ();

                TimelineData timelineData = new TimelineData (countryId, startingDate.Date, deathsForDay, confirmedForDay);
                output.Add (timelineData);

                startingDate = startingDate.AddDays (1);
            }

            return output;
        }
    }
}