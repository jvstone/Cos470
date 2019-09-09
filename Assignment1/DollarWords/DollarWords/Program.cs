using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DollarWords
{
    class Program
    {
        const int CharacterOffset = (int)'A' - 1;
        const string InputFilePath = @"C:\Users\jenni\Desktop\Fall 2019\COS 470\Cos470\Assignment1\DollarWords\words.txt";
        static void Main(string[] args)
        {
            StreamWriter outputFile = new StreamWriter(@"C:\Users\jenni\Desktop\Fall 2019\COS 470\Cos470\Assignment1\DollarWords\DollarWords.txt");

            var words = readWords(InputFilePath);
           
            outputFile.Dispose();
            Console.Read();
        }   
        
        public static List<string> readWords(String path)
        {
            var words = new List<string>();
            using (StreamReader stream = new StreamReader(path))
            {
                while (!stream.EndOfStream)
                {
                    words.Add(stream.ReadLine());
                }
            }
            return words;
        }

        public static List<string> getDollarWords(List<string> words) 
        {
            return words.Where(w => StringToIntegerValue(w) == 100).ToList();
        }

        /// <summary>
        /// Returns an interger value of a character where letter a-z correspond to 0-26 and all other characters to 0.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static int StringToIntegerValue(String word)
        {
            int wordValue = 0;
            foreach (char character in word)
            {
                int charVal = 0;
                if (char.IsLetter(character))
                {
                    charVal = (int)Char.ToUpper(character) - CharacterOffset;
                }
                wordValue += charVal;
            }
            return wordValue;
        }
    }

}

