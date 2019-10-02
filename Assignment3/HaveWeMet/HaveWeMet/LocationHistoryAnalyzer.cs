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
            //return locations
            //        .Where(loc => (loc.date.Day == date.Day && loc.date.Month == date.Month && loc.date.Year == date.Year))
            //        .ToList();
        }

    
    }
}
