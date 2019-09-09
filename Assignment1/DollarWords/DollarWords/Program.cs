using System;
using System.IO;
namespace DollarWords
{
    class Program
    {
        const int CharacterOffset = (int)'A' - 1;
        static void Main(string[] args)
        {
            StreamReader file = new StreamReader(@"C:\Users\jenni\Desktop\Fall 2019\COS 470\Cos470\Assignment1\DollarWords\words.txt");
            StreamWriter outputFile = new StreamWriter(@"C:\Users\jenni\Desktop\Fall 2019\COS 470\Cos470\Assignment1\DollarWords\DollarWords.txt");
            int dollarWordCount = 0;
            while (!file.EndOfStream)
            {
                int wordValue = 0;
                String word = file.ReadLine();
                foreach(char c in word)
                {
                    wordValue += LetterToIntegerValue(c);
                }
                if(wordValue == 100)
                {
                    dollarWordCount++;
                    outputFile.WriteLine(word);
                }
             
            }
            Console.WriteLine(dollarWordCount + " dollar words");
            file.Dispose();
            outputFile.Close();
            Console.Read();
        }   
        

        /// <summary>
        /// Returns an interger value of a character where letter a-z correspond to 0-26 and all other characters to 0.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static int LetterToIntegerValue(char character)
        {
            int charVal = 0;
            if (char.IsLetter(character))
            {
               character =  Char.ToUpper(character);
                charVal = (int)character - CharacterOffset;
            }
            return charVal;
        }
    }
}

