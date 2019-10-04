using HaveWeMet;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class HaveWeMetUtilsTests
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

        [Test]
        public void TestGetDistanceBetweenSameLocationReturnsZero()
        {
            Location loc1 = new Location()
            {
                timestampMs = "1484242416215",
                latitudeE7 = 436615711,
                longitudeE7 = -702530221
            };

            Location loc2 = new Location()
            {
                timestampMs = "1484242416215",
                latitudeE7 = 436615711,
                longitudeE7 = -702530221
            };

            var result = HaveWeMetUtils.GetDistanceBetween(loc1, loc2);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void TestGetDistanceBetweenDifferentLocations()
        {
            Location loc1 = new Location()
            {
                timestampMs = "1484242416215",
                latitudeE7 = 536682431,
                longitudeE7 = -712530221
            };

            Location loc2 = new Location()
            {
                timestampMs = "1484242416215",
                latitudeE7 = 436615711,
                longitudeE7 = -702530221
            };

            var result = HaveWeMetUtils.GetDistanceBetween(loc1, loc2);
            
            Assert.AreEqual(1115000, result, 100);
        }

        [Test]
        public void TestCheckLocationsOverlapSameLocation()
        {
            Location loc1 = new Location()
            {
                timestampMs = "1484242416215",
                latitudeE7 = 436615711,
                longitudeE7 = -702530221
            };

            Location loc2 = new Location()
            {
                timestampMs = "1484242416215",
                latitudeE7 = 436615711,
                longitudeE7 = -702530221
            };

            var result = HaveWeMetUtils.CheckLocationsOverlap(loc1, loc2, 0);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestCheckLocationsOverlapCloseLocations()
        {
            Location loc1 = new Location()
            {
                timestampMs = "1484242416215",
                latitudeE7 = 450000000,
                longitudeE7 = -700001000
            };

            Location loc2 = new Location()
            {
                timestampMs = "1484242416215",
                latitudeE7 = 450001000,
                longitudeE7 = -700000000
            };

            var result = HaveWeMetUtils.CheckLocationsOverlap(loc1, loc2, 100);

            Assert.IsTrue(result);
        }
    }
}