using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoronavirusDailyReportApp.Core.Models {
    public class ReportModel {
        public string Header { get; set; }
        public List<NewLocation> Locations { get; set; }
        public DateTime ReportDate { get; set; }
        private readonly IReportValuesProvider _provider;

        public ReportModel (List<NewLocation> locations, DateTime reportDate, IReportValuesProvider provider) {
            Locations = locations;
            ReportDate = reportDate;
            _provider = provider;
            Header = CreateHeader ();
        }

        private string CreateHeader () {
            string header = $"Covid Cases For {_provider.GetReportTime()}";
            return header;
        }

        private int GetDiff (int newDateAmount, int oldDateAmount) => newDateAmount - oldDateAmount;

        private string DisplayDiff (int newamount, int oldamount) {

            if (newamount > oldamount) {
                return $"+{newamount - oldamount}";
            } else if (newamount < oldamount) {
                return $"{newamount - oldamount}";
            } else return "No Change";
        }

        public string SlackMessage {
            get {
                StringBuilder sb = new StringBuilder ();
                sb.AppendLine (Header);
                sb.AppendLine ();
                foreach (NewLocation location in Locations) {
                    sb.AppendLine ($"{location.Country}");

                    List<CovidStats> stats = location.DailyStats;

                    var deaths = stats.Select (p => p.Deaths).ToList ();
                    var confirmed = stats.Select (p => p.Confirmed).ToList ();

                    sb.AppendLine ($"{location.DailyStats[0].Deaths} Deaths ({DisplayDiff(deaths[0], deaths[1])})");
                    sb.AppendLine ($"{location.DailyStats[0].Confirmed} Confirmed ({DisplayDiff(confirmed[0], confirmed[1])})");
                    sb.AppendLine ();
                }

                return sb.ToString ();
            }
        }
    }
}