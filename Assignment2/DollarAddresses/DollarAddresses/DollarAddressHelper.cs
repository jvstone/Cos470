using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DollarAddresses
{
    class DollarAddressHelper
    {
        int characterOffset;


        public DollarAddressHelper(int characterOffset)
        {
            this.characterOffset = characterOffset;
        }

        /// <summary>
        /// Return a list of where the Streename has an integer value that matches the address number
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public List<Address> GetDollarAddresses(List<Address> addresses)
        {
            return addresses.Where(a => StringToIntegerValue((a.STREETNAME + a.SUFFIX)) == a.ADDRESS_NUMBER).ToList();
        }

        /// <summary>
        /// Returns an integer value of a character where letter a-z correspond to 1-26 and all other characters to 0.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public  int StringToIntegerValue(String address)
        {
            int wordValue = 0;
            foreach (char character in address)
            {
                int charVal = 0;
                if (char.IsLetter(character))
                {
                    charVal = (int)Char.ToUpper(character) - characterOffset;
                }
                wordValue += charVal;
            }
            return wordValue;
        }
    }
}
