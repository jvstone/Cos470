using System;
using System.Collections.Generic;
using System.Linq;

namespace DollarAddresses
{
    public class DollarAddressHelper
    {

        /// <summary>
        /// Return a list of where the Streename has an integer value that matches the address number
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static List<Address> GetDollarAddresses(List<Address> addresses)
        {
            return addresses.Where(a => StringToIntegerValue((a.STREETNAME + a.SUFFIX)) == a.ADDRESS_NUMBER).ToList();
        }

        /// <summary>
        /// Returns an integer value of a character where letter a-z correspond to 1-26 and all other characters to 0.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static int StringToIntegerValue(String address)
        {
            return address.Where(c => char.IsLetter(c)).Sum(c=> Char.ToUpper(c)-'A' + 1);
        }
    }
}
