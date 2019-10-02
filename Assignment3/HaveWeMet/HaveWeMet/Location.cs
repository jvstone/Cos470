using System;
using System.Collections.Generic;

namespace HaveWeMet
{
    public class Location
    {
        public string timestampMs { get; set; }
        public long latitudeE7 { get; set; }
        public long longitudeE7 { get; set; }
        public DateTimeOffset date
        {
            get {
                long ms;
                long.TryParse(timestampMs, out ms);
                return DateTimeOffset.FromUnixTimeMilliseconds(ms);
            }
            
        }
    }

    public class Locations
    {
        public List<Location> locations {get; set;}
    }
}
