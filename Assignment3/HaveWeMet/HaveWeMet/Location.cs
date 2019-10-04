using System;
using System.Collections.Generic;

namespace HaveWeMet
{
    public class Location
    {
        private const double E7Conversion = .0000001;
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

        public double latitude
        {
            get {
                return latitudeE7 * E7Conversion;
            }
        }

        public double longitude{
            get{
                return longitudeE7 * E7Conversion;
                
            }
        }
    }

    public class Locations
    {
        public List<Location> locations {get; set;}
    }
}
