using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

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

        public List<Address> GetAddressesFromApi()
        {
            var urlAddress = string.Format(url, city);

            using(var client = new HttpClient())
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

        private List<Address> AddressDeserializer(string json)
        {
            
            var root =  JsonConvert.DeserializeObject<RootObject>(json);
            var addresses = root.features.Select(f => f.attributes);
            return addresses.ToList();
        }

    }
}
