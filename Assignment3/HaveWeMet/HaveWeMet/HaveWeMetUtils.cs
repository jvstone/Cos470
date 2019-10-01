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

    
    }
}
