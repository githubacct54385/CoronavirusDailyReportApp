using System;

namespace CoronavirusDailyReportApp.Core.Models {
    public class CovidDates {
        public CovidDates (DateTime newDate, DateTime oldDate) {
            if (newDate <= oldDate) {
                throw new ArgumentException ("firstDate argument must be less than secondDate argument.");
            }
            NewDate = newDate;
            OldDate = oldDate;
        }

        public DateTime NewDate { get; set; }
        public DateTime OldDate { get; set; }
    }
}