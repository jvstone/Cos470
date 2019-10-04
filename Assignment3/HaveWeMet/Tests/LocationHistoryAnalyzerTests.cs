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
            var results = LocationHistoryAnalyzer.CheckAlibi(new DateTimeOffset(new DateTime(2017, 1, 12)), locations);
            Assert.AreEqual(2, results.Count);
            CollectionAssert.Contains(results, locations[0]);
            CollectionAssert.Contains(results, locations[1]);
            CollectionAssert.DoesNotContain(results, locations[2]);

        }

        [Test]
        public static void CheckHaveWeMetWhenTimesAndLocationsMatch()
        {
            var locations1 = new List<Location> { new Location(){ timestampMs = "1484242416215",
                                                                 latitudeE7 =  436615711,
                                                                 longitudeE7 = -702530221},
                                                new Location(){timestampMs = "1554074042394",
                                                               latitudeE7 = 436611851,
                                                               longitudeE7 = -702512553},
                                                 new Location(){timestampMs = "1554074042999",
                                                               latitudeE7 = 436611851,
                                                               longitudeE7 = -702512553}
                                                };

            var locations2 = new List<Location> { new Location(){ timestampMs = "1484242416215",
                                                                 latitudeE7 =  436615711,
                                                                 longitudeE7 = -702530221},                                                
                                                  new Location(){timestampMs = "1554074042394",
                                                               latitudeE7 = 436611851,
                                                               longitudeE7 = -702512553},
                                                  new Location(){timestampMs = "1554074042999",
                                                                 latitudeE7 = 436611851,
                                                                 longitudeE7 = -702512553}
                                                  };

            var results = LocationHistoryAnalyzer.HaveWeMet(locations1, locations2);
            Assert.AreEqual(3, results.Count);
            CollectionAssert.AreEquivalent(locations1, results);
        }

        [Test]
        public static void CheckHaveWeMetWhenWithDifferentTimes()
        {
            var locations1 = new List<Location> { new Location(){ timestampMs = "1484242416215",
                                                                 latitudeE7 =  436615711,
                                                                 longitudeE7 = -702530221},
                                                new Location(){timestampMs = "1554074010000",
                                                               latitudeE7 = 436611851,
                                                               longitudeE7 = -702512553},
                                                 new Location(){timestampMs = "1554074042999",
                                                               latitudeE7 = 436611851,
                                                               longitudeE7 = -702512553}
                                                };

            var locations2 = new List<Location> { new Location(){ timestampMs = "1484242417215",
                                                                 latitudeE7 =  436615711,
                                                                 longitudeE7 = -702530221},
                                                  new Location(){timestampMs = "1554074042394",
                                                               latitudeE7 = 436611851,
                                                               longitudeE7 = -702512553},
                                                  new Location(){timestampMs = "1554074042999",
                                                                 latitudeE7 = 436611851,
                                                                 longitudeE7 = -702512553}
                                                  };

            var results = LocationHistoryAnalyzer.HaveWeMet(locations1, locations2, 1000);
            Assert.AreEqual(2, results.Count);
            CollectionAssert.Contains(results, locations1[0]);
            CollectionAssert.Contains(results, locations1[2]);
            CollectionAssert.DoesNotContain(results, locations1[1]);
        }

        [Test]
        public static void CheckHaveWeMetWithDifferentLocations()
        {
            var locations1 = new List<Location> { new Location(){ timestampMs = "1484242416215",
                                                                 latitudeE7 =  436615711,
                                                                 longitudeE7 = -702530221},
                                                new Location(){timestampMs = "1554074042394",
                                                               latitudeE7 = 436611851,
                                                               longitudeE7 = -702512553},
                                                 new Location(){timestampMs = "1554074042999",
                                                               latitudeE7 = 436611851,
                                                               longitudeE7 = -702512553}
                                                };

            var locations2 = new List<Location> { new Location(){ timestampMs = "1484242416215",
                                                                 latitudeE7 =  436615111,
                                                                 longitudeE7 = -702530221},
                                                  new Location(){timestampMs = "1554074042394",
                                                               latitudeE7 = 446611851,
                                                               longitudeE7 = -702512553},
                                                  new Location(){timestampMs = "1554074042999",
                                                                 latitudeE7 = 436611851,
                                                                 longitudeE7 = -702512553}
                                                  };

            var results = LocationHistoryAnalyzer.HaveWeMet(locations1, locations2, distanceOffset: 100);
            Assert.AreEqual(2, results.Count);
            CollectionAssert.Contains(results, locations1[0]); 
            CollectionAssert.Contains(results, locations1[2]);
        }

        [Test]
        public static void CheckHaveWeMetWhenTimesAndLocationsDoNotMatch()
        {
            var locations1 = new List<Location> { new Location(){ timestampMs = "1484242426215",
                                                                 latitudeE7 =  436615711,
                                                                 longitudeE7 = -702530321},
                                                new Location(){timestampMs = "1554074042394",
                                                               latitudeE7 = 436611851,
                                                               longitudeE7 = -702512553},
                                                 new Location(){timestampMs = "1554074042999",
                                                               latitudeE7 = 436611851,
                                                               longitudeE7 = -702512553}
                                                };

            var locations2 = new List<Location> { new Location(){ timestampMs = "1484242416215",
                                                                 latitudeE7 =  436615711,
                                                                 longitudeE7 = -702530221},
                                                  new Location(){timestampMs = "1554074042394",
                                                               latitudeE7 = 436611851,
                                                               longitudeE7 = -702512553},
                                                  new Location(){timestampMs = "1554074042999",
                                                                 latitudeE7 = 436611851,
                                                                 longitudeE7 = -702512553}
                                                  };

            var results = LocationHistoryAnalyzer.HaveWeMet(locations1, locations2, 1000000, 100);
            Assert.AreEqual(3, results.Count);
            CollectionAssert.AreEquivalent(locations1, results);
        }


    }
}
