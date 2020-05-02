using System;

namespace CoronavirusDailyReportApp.Core.Models {
    public class CovidRequestPayload {
        public Location location { get; set; }
    }

    public class Location {
        public Location () {

        }
        public Location (Latest latest, int id, string country) {
            this.latest = latest;
            this.id = id;
            this.country = country;
        }

        public Latest latest { get; set; }
        public int id { get; set; }
        public string country { get; set; }

    }
    public class Latest : IEquatable<Latest> {
        public int deaths { get; set; }
        public int confirmed { get; set; }

        public Latest () {

        }
        public Latest (int deaths, int confirmed) {
            this.deaths = deaths;
            this.confirmed = confirmed;
        }

        public bool Equals (Latest other) {
            return this.deaths == other.deaths && this.confirmed == other.confirmed;
        }
    }
}