using System;
using System.Collections.Generic;
using System.Linq;

namespace HaveWeMet
{
    public static class LocationHistoryAnalyzer
    {
        
        /// <summary>
        /// Return a list of all the locations for a specific date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="locations"></param>
        /// <returns></returns>
        public static List<Location> CheckAlibi(DateTimeOffset date, List<Location> locations)
        {
            var results = new List<Location>();
            var nextDay = date.AddDays(1);
            foreach(Location loc in locations)
            {
                if (loc.date.Day == date.Day && loc.date.Month == date.Month && loc.date.Year == date.Year)
                    results.Add(loc);
                //Break out early if date of the current location after the date that is being searched for
                if (loc.date >= nextDay)
                    break;
            }

            return results;
        }

        /// <summary>
        /// Checks if two sets of order locations (times in ascending order) have overlaps in time and location. Returns a list of the first matches that 
        /// occur for a given time interval and matches are always added from the location1 list. 
        /// </summary>
        /// <param name="locations1"></param>
        /// <param name="locations2"></param>
        /// <param name="timeOffest">In milliseconds</param>
        /// <param name="distanceOffset">In meters</param>
        /// <returns></returns>
        public static List<Location> HaveWeMet(List<Location> locations1, List<Location> locations2, long timeOffest = 0 , double distanceOffset = 0)
        {
            var results = new List<Location>();
            var index1 = 0 ;
            var index2 = 0;
           while(index1 < locations1.Count && index2 < locations2.Count)
            {
                if(HaveWeMetUtils.CheckLocationsOverlap(locations1[index1],locations2[index2], distanceOffset) 
                    && HaveWeMetUtils.CheckTimesOverlap(locations1[index1].date, locations2[index2].date, timeOffest))
                {
                    results.Add(locations1[index1]);
                    index1++; 
                    index2++;
                }
                else
                {
                    if( locations1[index1].date  < locations2[index2].date)
                    {
                        index1++;
                    }
                    else
                    {
                        index2++;
                    }
                }
            }

            return results;
        }

    
    }
}
