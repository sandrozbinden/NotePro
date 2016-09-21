using System;
using NotePro.ExtensionMethods;
using Xunit;

namespace NotePro.Tests
{
    public class DateTimeToTextTest
    {
        [Fact]
        public void TestDateTimeToday()
        {
            Assert.Equal("Today", DateTime.Now.ToText()); ;
        }
    }
}
