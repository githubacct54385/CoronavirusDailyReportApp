using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomDateTimeExtension.Core;

namespace CoronavirusDailyReportApp.Core.Models {
    public class ReportModel {
        public List<Location> Locations { get; set; }
        private readonly IReportValuesProvider _provider;

        public ReportModel (List<Location> locations, IReportValuesProvider provider) {
            Locations = locations;
            _provider = provider;
        }

        // private string CreateHeader () => $"Covid Cases For {_provider.GetReportTime()}";
        private string CreateHeader () {
            if (Locations.Any () && Locations.First ().TimelineData.Count >= 2) {
                Location firstLocation = Locations.First ();
                var orderedTimelineData = firstLocation.TimelineData.OrderByDescending (p => p.TimelineDate).ToList ();
                var latestTimeline = orderedTimelineData.First ();
                return $"Covid Cases For {latestTimeline.TimelineDate.CustomToString()}";
            }
            throw new Exception ("No Locations...");
        }

        private string CreateSubHeader () {
            if (Locations.Any () && Locations.First ().TimelineData.Count >= 2) {
                Location firstLocation = Locations.First ();
                var orderedLocations = firstLocation.TimelineData.OrderByDescending (p => p.TimelineDate).ToList ();
                var secondToLastTimeline = orderedLocations[1];
                return $"Comparing with {secondToLastTimeline.TimelineDate.CustomToString()}";
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

                    sb.AppendLine ();
                }

                return sb.ToString ();
            }
        }
    }
}