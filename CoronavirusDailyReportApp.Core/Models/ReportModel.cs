using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoronavirusDailyReportApp.Core.Models {
    public class ReportModel {
        public string Header { get; set; }
        public List<Location> Locations { get; set; }
        private readonly IReportValuesProvider _provider;

        public ReportModel (List<Location> locations, IReportValuesProvider provider) {
            Locations = locations;
            _provider = provider;
        }

        private string CreateHeader () {
            string header = $"Covid Cases For {_provider.GetReportTime()}";
            return header;
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
                sb.AppendLine ();
                foreach (Location location in Locations) {
                    sb.AppendLine ($"{location.Country}");

                    List<CovidStats> stats = location.DailyStats;

                    var deaths = stats.Select (p => p.Deaths).ToList ();
                    var confirmed = stats.Select (p => p.Confirmed).ToList ();

                    sb.AppendLine ($"{Formatted(location.DailyStats[0].Deaths)} Deaths ({DisplayDiff(deaths[0], deaths[1])})");
                    sb.AppendLine ($"{Formatted(location.DailyStats[0].Confirmed)} Confirmed ({DisplayDiff(confirmed[0], confirmed[1])})");
                    sb.AppendLine ();
                }

                return sb.ToString ();
            }
        }
    }
}