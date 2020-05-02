using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoronavirusDailyReportApp.Core.Models {
    public class ReportModel {
        public DateTime CompareDate { get; set; }
        public List<Location> Locations { get; set; }
        private readonly IReportValuesProvider _provider;

        public ReportModel (List<Location> locations, DateTime compareDate, IReportValuesProvider provider) {
            Locations = locations;
            CompareDate = compareDate;
            _provider = provider;
        }

        // private string CreateHeader () => $"Covid Cases For {_provider.GetReportTime()}";
        private string CreateHeader () {
            if (Locations.Any () && Locations.First ().TimelineData.Any ()) {
                var orderedTimelineData = Locations.First ().TimelineData.OrderByDescending (p => p.TimelineDate).ToList ();
                var latest = orderedTimelineData.First ();
                return $"Covid Cases For {latest.TimelineDate.ToString("dddd MMMM dd, yyyy")}";
            }
            throw new Exception ("No Locations...");
        }

        private string CreateSubHeader () {
            if (Locations.Any () && Locations.First ().TimelineData.Any ()) {
                var orderedLocations = Locations.First ().TimelineData.OrderByDescending (p => p.TimelineDate).ToList ();
                var secondToLast = orderedLocations[1];
                return $"Comparing with {secondToLast.TimelineDate.ToString("dddd MMMM dd, yyyy")}";
            }
            throw new Exception ("No Locations...");
        }

        private string Formatted (int number) {
            return String.Format ("{0:n0}", number);
        }

        private string DisplayDiff (int newamount, int oldamount) {

            if (newamount > oldamount) {
                return $"+{Formatted(newamount - oldamount)}";
            } else if (newamount < oldamount) {
                return $"{Formatted(newamount - oldamount)}";
            } else return "No Change";
        }

        public string SlackMessage {
            get {
                StringBuilder sb = new StringBuilder ();
                sb.AppendLine (CreateHeader ());
                sb.AppendLine (CreateSubHeader ());
                sb.AppendLine ();
                foreach (Location location in Locations) {
                    sb.AppendLine ($"{location.Country}");

                    List<TimelineData> timelineData = location.TimelineData;

                    var reverseTimeline = timelineData.OrderByDescending (p => p.TimelineDate).ToList ();

                    var newest = reverseTimeline.First ();
                    var dayBefore = reverseTimeline[1];

                    sb.Append (Formatted (newest.Deaths));
                    sb.Append (" Deaths (");
                    sb.Append (DisplayDiff (newest.Deaths, dayBefore.Deaths));
                    sb.Append (")\n");

                    sb.Append (Formatted (newest.Confirmed));
                    sb.Append (" Confirmed (");
                    sb.Append (DisplayDiff (newest.Confirmed, dayBefore.Confirmed));
                    sb.Append (")\n");

                    // var deaths = stats.Select (p => p.Deaths).ToList ();
                    // var confirmed = stats.Select (p => p.Confirmed).ToList ();

                    // sb.AppendLine ($"{Formatted(location.DailyStats[0].Deaths)} Deaths ({DisplayDiff(deaths[0], deaths[1])})");
                    // sb.AppendLine ($"{Formatted(location.DailyStats[0].Confirmed)} Confirmed ({DisplayDiff(confirmed[0], confirmed[1])})");
                    sb.AppendLine ();
                }

                return sb.ToString ();
            }
        }
    }
}