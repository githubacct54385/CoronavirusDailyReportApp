using System;
using Xunit;

namespace CoronavirusAzFunction.Tests {
    public class DivisionTests {
        [Fact]
        public void DivideTest_DividesCorrectly () {
            double expected = 2;
            double actual = 6 / 3;

            Assert.Equal (expected, actual);
        }
    }
}