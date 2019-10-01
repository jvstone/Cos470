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

        public static bool CheckTimesOverlap(DateTimeOffset date1, DateTimeOffset date2, long offset = 0)
        {
            return Math.Abs((date1 - date2).TotalMilliseconds) <= offset;      
        }
    }
}
