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
            var dollarAddresses = new DollarAddressHelper(offset);
        }
    }
}
