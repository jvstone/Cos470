using HaveWeMet;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestDeserializer()
        {
            var json = @"{""locations"" : [ {
                        ""timestampMs"" : ""1484242416215"",
                        ""latitudeE7"" : 436615711,
                        ""longitudeE7"" : -702530221,
                        ""accuracy"" : 1299,
                        ""activity"" : [ {
                         ""timestampMs"" : ""1484242435764"",
                         ""activity"" : [ {
                            ""type"" : ""TILTING"",
                            ""confidence"" : 100
                          } ]
                        } ]
                      }]}";
            List<Location> locations = HaveWeMetUtils.DeserializeJsonLocationHistory(json);
            var location = locations[0];
            Assert.AreEqual(location.timestampMs, "1484242416215");
            Assert.AreEqual(location.latitudeE7, 436615711);
            Assert.AreEqual(location.longitudeE7, -702530221); 
        }

        [Test]
        public void TestCheckTimesOverlapWithSameTimes()
        {
            DateTimeOffset date1 = DateTimeOffset.FromUnixTimeMilliseconds(1000);
            DateTimeOffset date2 = DateTimeOffset.FromUnixTimeMilliseconds(1000);

            bool inRange = HaveWeMetUtils.CheckTimesOverlap(date1, date2);
            Assert.IsTrue(inRange);
        }

        [Test]
        public void TestCheckTimesOverlapWithDifferentOverlappingTimes()
        {
            DateTimeOffset date1 = DateTimeOffset.FromUnixTimeMilliseconds(1000);
            DateTimeOffset date2 = DateTimeOffset.FromUnixTimeMilliseconds(2000);

            bool inRange = HaveWeMetUtils.CheckTimesOverlap(date1, date2, 1000);
            Assert.IsTrue(inRange);
        }

        [Test]
        public void TestCheckTimesOverlapWithDifferentNonOverlappingTimes()
        {
            DateTimeOffset date1 = DateTimeOffset.FromUnixTimeMilliseconds(1000);
            DateTimeOffset date2 = DateTimeOffset.FromUnixTimeMilliseconds(3000);

            bool inRange = HaveWeMetUtils.CheckTimesOverlap(date1, date2, 1000);
            Assert.IsFalse(inRange);
        }
    }
}