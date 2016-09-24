using System;
using NotePro.ExtensionMethods;
using Xunit;

namespace NotePro.Tests.ExtensionMethods
{
    public class DateTimeToTextTest
    {
        [Fact]
        public void TestDateInThePast()
        {
            Assert.Equal("In the Past", DateTime.Today.AddDays(-31).ToText()); ;
        }


        [Fact]
        public void TestDateTime3DaysAgo()
        {
            Assert.Equal("3 Days ago", DateTime.Today.AddDays(-3).ToText()); ;
        }

        [Fact]
        public void TestDateTimeToday()
        {
            Assert.Equal("Today", DateTime.Now.ToText()); ;
        }

        [Fact]
        public void TestDateTimeYesterday()
        {
            Assert.Equal("Yesterday", DateTime.Now.AddDays(-1).ToText()); ;
        }

        [Fact]
        public void TestDateTimeTomorrow()
        {
            Assert.Equal("Tomorrow", DateTime.Now.AddDays(1).ToText()); ;
        }

        [Fact]
        public void TestDateTimeIn3Days()
        {
            Assert.Equal("In 3 Days", DateTime.Today.AddDays(3).ToText()); ;
        }

        [Fact]
        public void TestDateInTheFuture()
        {
            Assert.Equal("In the Future", DateTime.Today.AddDays(31).ToText()); ;
        }

    }
}
