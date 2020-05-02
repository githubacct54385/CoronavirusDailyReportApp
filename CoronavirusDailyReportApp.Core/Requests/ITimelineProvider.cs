using System;
using System.Collections.Generic;
using CoronavirusDailyReportApp.Core.Models;
using CoronavirusDailyReportApp.Core.Utils;
using Newtonsoft.Json.Linq;

namespace CoronavirusDailyReportApp.Core.Requests {
    public interface ITimelineProvider {
        List<TimelineData> TimelineData (string json);
    }

    public sealed class TimelineProviderImpl : ITimelineProvider {
        public List<TimelineData> TimelineData (string json) {

            List<TimelineData> output = new List<TimelineData> ();

            JObject covidData = JObject.Parse (json);

            int countryId = covidData.GetValue ("location") ["id"].Value<int> ();

            JToken confirmedTimelines = covidData.GetValue ("location") ["timelines"]["confirmed"]["timeline"];
            JToken deathTimelines = covidData.GetValue ("location") ["timelines"]["deaths"]["timeline"];

            DateTime startingDate = new DateTime (2020, 1, 22);
            while (startingDate < DateTime.Today) {
                string dateToken = DateUtils.GetDateToken (startingDate);

                int confirmed = confirmedTimelines[dateToken].Value<int> ();
                int deaths = deathTimelines[dateToken].Value<int> ();

                TimelineData timelineData = new TimelineData (countryId, startingDate.Date, deaths, confirmed);
                output.Add (timelineData);

                startingDate = startingDate.AddDays (1);
            }

            return output;
        }
    }
}