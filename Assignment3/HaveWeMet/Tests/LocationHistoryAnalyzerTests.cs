using HaveWeMet;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    class LocationHistoryAnalyzerTests
    {
        [Test]
        public static void CheckAlibiReturnsLocationsForDateTest()
        {
            var locations = new List<Location> { new Location(){ timestampMs = "1484242416215",
                                                                 latitudeE7 =  436615711,
                                                                 longitudeE7 = -702530221},
                                                new Location(){ timestampMs = "1484242459563",
                                                                latitudeE7 = 436615711,
                                                                longitudeE7 = -702530221},
                                                new Location(){timestampMs = "1554074042394",
                                                               latitudeE7 = 436611851,
                                                               longitudeE7 = -702512553},
                                                 new Location(){timestampMs = "1554074042999",
                                                               latitudeE7 = 436611851,
                                                               longitudeE7 = -702512553}
                                                };
            var results = LocationHistoryAnalyzer.CheckAlibi(new DateTimeOffset(new DateTime(2017,1,12)), locations);
            Assert.AreEqual(2, results.Count);
            CollectionAssert.Contains(results, locations[0]);
            CollectionAssert.Contains(results, locations[1]);
            CollectionAssert.DoesNotContain(results, locations[2]);

        }

    }
}
