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
        /// Return a list of words whose value sums to 100 
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public List<string> GetDollarWords(List<string> words)
        {
            return words.Where(w => StringToIntegerValue(w) == 100).ToList();
        }

        /// <summary>
        /// Returns an integer value of a character where letter a-z correspond to 1-26 and all other characters to 0.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public  int StringToIntegerValue(String word)
        {
            int wordValue = 0;
            foreach (char character in word)
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
