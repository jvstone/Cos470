using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DollarWords
{
    class Program
    {
        const int CharacterOffset = (int)'A' - 1;
        const string InputFilePath = @"C:\Users\jenni\Desktop\Fall 2019\COS 470\Cos470\Assignment1\DollarWords\words.txt";
        const string OutputFilePath = @"C:\Users\jenni\Desktop\Fall 2019\COS 470\Cos470\Assignment1\DollarWords\DollarWords.txt";

        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var words = ReadWords(InputFilePath);
            var dollarWords = GetDollarWords(words);
            OutputToFile(OutputFilePath, dollarWords);
            OutputAggregateDataToConsole(words, dollarWords);

            stopwatch.Stop();
            Console.WriteLine("Total elapsed time: {0} ms", stopwatch.ElapsedMilliseconds);
            Console.Read();
        }
        /// <summary>
        /// Reads words from a file to a list where each word is written on a new line
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> ReadWords(String path)
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

        /// <summary>
        /// Return a list of words whose value sums to 100 
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static List<string> GetDollarWords(List<string> words)
        {
            return words.Where(w => StringToIntegerValue(w) == 100).ToList();
        }

        /// <summary>
        /// Get the most expensive word based on letter value defined by StringToIntegerValue
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static string GetMostExpensiveWord(List<string> words)
        {
            return words.OrderByDescending(w => StringToIntegerValue(w)).First();
        }

        /// <summary>
        /// Get the longest word 
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static string GetLongestWord(List<String> words)
        {
            return words.OrderByDescending(w => w.Length).First();
        }

        /// <summary>
        /// Get the shortest word
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static string GetShortestWord(List<String> words)
        {
            return words.OrderBy(w => w.Length).First();
        }

        /// <summary>
        /// Returns an integer value of a character where letter a-z correspond to 1-26 and all other characters to 0.
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

        /// <summary>
        /// Output aggregate information
        /// </summary>
        /// <param name="words"></param>
        /// <param name="dollarWords"></param>
        public static void OutputAggregateDataToConsole(List<string> words, List<string> dollarWords)
        {
            Console.WriteLine("Portion of words that are dollar words: {0:P4}", (double)dollarWords.Count / words.Count);
            Console.WriteLine("Longest dollar word: {0}", GetLongestWord(dollarWords));
            Console.WriteLine("Shortest dollar word: {0}", GetShortestWord(dollarWords));
            Console.WriteLine("Most Expensive word: {0}", GetMostExpensiveWord( words));
        }

        /// <summary>
        /// Output a list to the file specified by outputPath. One entry per line. 
        /// </summary>
        /// <param name="outputPath"></param>
        /// <param name="words"></param>
        public static void OutputToFile(string outputPath, List<string> words)
        {
            using (StreamWriter outputFile = new StreamWriter(outputPath))
            {
                foreach(string word in words){
                    outputFile.WriteLine(word);
                }
            }
        }
    }

}

