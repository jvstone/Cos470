using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DollarAddresses
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                              .AddJsonFile("appsettings.json", false, true)
                              .Build();

            var offset = int.Parse( config["CharOffset"]);
            var client = new AddressApiClient(config["URL"], config["City"]);

            var dollarAddresses = DollarAddressHelper.GetDollarAddresses(client.GetAddressesFromApi());
            OutputAddressesToConsole(dollarAddresses, config["City"]);
        }

        public static void OutputAddressesToConsole(List<Address> addresses, string city)
        {
            Console.WriteLine($"The following are dollar addresses in {city}");
            foreach(var address in addresses)
            {
                Console.WriteLine($"{address.ADDRESS_NUMBER} {address.STREETNAME} {address.SUFFIX}");
            }
        }
        

        
    }
}
