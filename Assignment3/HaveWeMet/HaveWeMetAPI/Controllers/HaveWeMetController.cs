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
        static IConfiguration config;
        public HaveWeMetController()
        {
            if (locationHistories.Count == 0)
            {
                string json;
                config = new ConfigurationBuilder()
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
        public ActionResult<List<Location>> Get(string id)
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

        // GET: api/HaveWeMet/{name}/WhereWasI/{YYYY-MM-DD}
        [HttpGet("{id}/WhereWasI/{date}")]
        public ActionResult<List<Location>> Get(string id, DateTimeOffset date)
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


        // GET: api/HaveWeMet/{name1}/HasMet/{Name2}
        [HttpGet("{id1}/HasMet/{id2}")]
        public ActionResult<List<Location>> Get(string id1, string id2)
        {
            if (locationHistories.ContainsKey(id1) && locationHistories.ContainsKey(id2))
            {
                long.TryParse(config["TimeOffset"], out long timeOffset);
                long.TryParse(config["DistanceOffset"], out long distanceOffset);
                return LocationHistoryAnalyzer.HaveWeMet(locationHistories[id1], locationHistories[id2], timeOffset, distanceOffset);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        // POST: api/HaveWeMet/name
        [HttpPost("{id}")]
        public ActionResult<string> Post(string id, [FromBody] Locations locs)
        {
            if (!locationHistories.ContainsKey(id))
            {
                locationHistories.Add(id, locs.locations);
                return id;
            }
            return new ConflictResult();
        }

        // PUT: api/HaveWeMet/name
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
