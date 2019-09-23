using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Web;

namespace DollarAddresses
{
    class AddressApiClient
    {
        string url, city;

        public AddressApiClient(string url, string city)
        {
            this.url = url;
            this.city = city;
        }

        /// <summary>
        /// Return a list of Addresses from an API. 
        /// </summary>
        /// <returns></returns>
        public List<Address> GetAddressesFromApi()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["where"] = $"MUNICIPALITY='{city}'";
            query["outfields"] = "ADDRESS_NUMBER,STREETNAME,SUFFIX";
            query["f"] = "pjson";
            var urlAddress = url + query;

            using (var client = new HttpClient())
            {
                using(HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlAddress))
                {
                    var response = client.SendAsync(request).Result;
                    var content = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"{content}: {response.StatusCode}");
                    }
                    return AddressDeserializer(content);
                }
            }
        }

        /// <summary>
        /// Convert JSON respresentation of address to objects.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private List<Address> AddressDeserializer(string json)
        {
            var root =  JsonConvert.DeserializeObject<RootObject>(json);
            var addresses = root.features.Select(f => f.attributes);
            return addresses.ToList();
        }

    }
}
