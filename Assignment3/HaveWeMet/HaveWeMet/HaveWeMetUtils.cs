using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaveWeMet
{
    public class HaveWeMetUtils
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<Location> DeserializeJsonLocationHistory(String json)
        {
            return JsonConvert.DeserializeObject<Locations>(json).locations;
        }

        public static DateTimeOffset MillisecondsToDateTime(string milliseconds)
        {
            long ms;
            long.TryParse(milliseconds, out ms);
            var date = DateTimeOffset.FromUnixTimeMilliseconds(ms);
            return date;
        }

        /// <summary>
        /// Checks if two times overlap within a certain threshold
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="offset">In milliseconds</param>
        /// <returns></returns>
        public static bool CheckTimesOverlap(DateTimeOffset date1, DateTimeOffset date2, long offset = 0)
        {
            return Math.Abs((date1 - date2).TotalMilliseconds) <= offset;      
        }

        /// <summary>
        /// Use haversine formula to determine the distance between two coordinates
        /// and check if the distance is within a threshhold in meters
        /// </summary>
        /// <returns></returns>
        public static bool CheckLocationsOverlap(Location loc1, Location loc2, double threshold)
        {
            var distance = GetDistanceBetween(loc1, loc2);
            return distance <= threshold;
        }


        /// <summary>
        /// Use the Haversine formulat to determine the distance in meters between
        /// two points. 
        /// </summary>
        /// <param name="loc1"></param>
        /// <param name="loc2"></param>
        /// <returns></returns>
        public static double GetDistanceBetween(Location loc1, Location loc2)
        {
            var radius = 6371000; //Radius in km
            var distanceLat = DegreesToRadians(loc1.latitude - loc2.latitude);
            var distanceLong = DegreesToRadians(loc1.longitude - loc2.longitude);

            double a = Math.Sin(distanceLat / 2) * Math.Sin(distanceLat / 2)
                        + Math.Cos(DegreesToRadians(loc1.latitude)) * Math.Cos(DegreesToRadians(loc2.latitude))
                        * Math.Sin(distanceLong / 2) * Math.Sin(distanceLong / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return radius * c;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }

}
