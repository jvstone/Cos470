using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HaveWeMet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HaveWeMetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HaveWeMetController : ControllerBase
    {
        static Dictionary<String, List<Location>> locationHistories = new Dictionary<String, List<Location>>();
        public HaveWeMetController()
        {
            if (locationHistories.Count == 0)
            {
                string json;
                var config = new ConfigurationBuilder()
                                  .AddJsonFile("appsettings.json", false, true)
                                  .Build();
                using (StreamReader stream = new StreamReader(config["DataPath"]))
                {
                    json = stream.ReadToEnd();
                }
                locationHistories.Add("Jen", HaveWeMetUtils.DeserializeJsonLocationHistory(json));
            }

        }
        // GET: api/HaveWeMet
        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            if (locationHistories.Count != 0)
            {
                return locationHistories.Keys.ToList();
            }
            else
            {
                return new NotFoundResult();
            }
        }

        // GET: api/HaveWeMet/name
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<List<Location>> Get(String id)
        {
            if (locationHistories.ContainsKey(id))
            {
                return locationHistories[id];
            }
            else
            {
                return new NotFoundResult();
            }
        }

        
        [HttpGet("{id}/WhereWasI/{date}")]
        public ActionResult<List<Location>> Get(String id, DateTimeOffset date)
        {
            if (locationHistories.ContainsKey(id))
            {
                return LocationHistoryAnalyzer.CheckAlibi(date, locationHistories[id]);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        // POST: api/HaveWeMet
        [HttpPost("{id}")]
        public void Post(String id, [FromBody] Locations locs)
        {
            if (!locationHistories.ContainsKey(id))
            {
                locationHistories.Add(id, locs.locations);
            }
        }

        // PUT: api/HaveWeMet/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Locations loc)
        {
            if (locationHistories.ContainsKey(id))
            {
                locationHistories[id] = loc.locations;
            }
            else
            {
                locationHistories.Add(id, loc.locations);
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            if (locationHistories.ContainsKey(id))
            {
                locationHistories.Remove(id);
            }
        }
    }
}
