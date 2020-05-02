using System;
using System.Collections.Generic;
using Autofac.Extras.Moq;
using CoronavirusDailyReportApp.Core.Models;
using CoronavirusDailyReportApp.Core.Requests;
using Newtonsoft.Json.Linq;
using Xunit;

namespace CoronavirusDailyReportApp.Tests {
    public class CreateTimelineTests {
        [Fact]
        public void TimeLine_ReturnsCorrectResults () {

            List<TimelineData> timelineData = new List<TimelineData> ();
            timelineData.Add (new TimelineData (225, new DateTime (2020, 4, 29), deaths : 60967, confirmed : 1039909));
            timelineData.Add (new TimelineData (225, new DateTime (2020, 4, 30), deaths : 62996, confirmed : 1069424));
            timelineData.Add (new TimelineData (225, new DateTime (2020, 5, 1), deaths : 64943, confirmed : 1103461));

            string json = SampleTimelineData.SampleTimelineJson ();

            using (AutoMock mock = AutoMock.GetLoose ()) {

                mock.Mock<ITimelineProvider> ()
                    .Setup (x => x.TimelineData (json))
                    .Returns (timelineData);

                Timeline sut = mock.Create<Timeline> ();
                List<TimelineData> actual = sut.Create (json);

                for (int i = 0; i < timelineData.Count; i++) {
                    Assert.Equal (timelineData[i].CountryId, actual[i].CountryId);
                    Assert.Equal (timelineData[i].TimelineDate, actual[i].TimelineDate);
                    Assert.Equal (timelineData[i].Deaths, actual[i].Deaths);
                    Assert.Equal (timelineData[i].Confirmed, actual[i].Confirmed);
                }

            }
        }
    }
}