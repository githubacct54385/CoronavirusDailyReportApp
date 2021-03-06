using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomDateTimeExtension;

namespace CoronavirusDailyReportApp.Core.Models {
    public class ReportModel {
        public List<Location> Locations { get; set; }

        public ReportModel (List<Location> locations) {
            Locations = locations;
        }

        private string CreateSlackHeader () {
            if (Locations.Any () && Locations.First ().TimelineData.Count >= 2) {
                Location firstLocation = Locations.First ();
                var orderedTimelineData = firstLocation.TimelineData.OrderByDescending (p => p.TimelineDate).ToList ();
                var latestTimeline = orderedTimelineData.First ();
                return $"Covid Cases For {latestTimeline.TimelineDate.CustomToString(false)}";
            }
            throw new Exception ("No Locations...");
        }

        private string CreateSlackSubHeader () {
            if (Locations.Any () && Locations.First ().TimelineData.Count >= 2) {
                Location firstLocation = Locations.First ();
                var orderedLocations = firstLocation.TimelineData.OrderByDescending (p => p.TimelineDate).ToList ();
                var secondToLastTimeline = orderedLocations[1];
                return $"Comparing with {secondToLastTimeline.TimelineDate.CustomToString(false)}";
            }
            throw new Exception ("No Locations...");
        }

        private string FormattedNumber (int number) {
            return String.Format ("{0:n0}", number);
        }

        private string DisplayNumberDiff (int newamount, int oldamount) {

            if (newamount > oldamount) {
                return $"+{FormattedNumber(newamount - oldamount)}";
            } else if (newamount < oldamount) {
                return $"{FormattedNumber(newamount - oldamount)}";
            } else return "No Change";
        }

        public string SlackMessage {
            get {
                StringBuilder sb = new StringBuilder ();
                sb.AppendLine (CreateSlackHeader ());
                sb.AppendLine (CreateSlackSubHeader ());
                sb.AppendLine ();
                foreach (Location location in Locations) {
                    sb.AppendLine ($"{location.Country}");

                    List<TimelineData> timelineData = location.TimelineData;

                    var reverseTimeline = timelineData.OrderByDescending (p => p.TimelineDate).ToList ();

                    var newest = reverseTimeline.First ();
                    var dayBefore = reverseTimeline[1];

                    sb.Append (FormattedNumber (newest.Deaths));
                    sb.Append (" Deaths (");
                    sb.Append (DisplayNumberDiff (newest.Deaths, dayBefore.Deaths));
                    sb.Append (")\n");

                    sb.Append (FormattedNumber (newest.Confirmed));
                    sb.Append (" Confirmed (");
                    sb.Append (DisplayNumberDiff (newest.Confirmed, dayBefore.Confirmed));
                    sb.Append (")\n");

                    sb.AppendLine ();
                }

                return sb.ToString ();
            }
        }
    }
}