using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/**
 * This assignment uses Linq in order to sort out IEnumerables
 * according to specifications such as shortest word length or
 * most frequently occurring number
 * 
 * Thuy Nguyen
 * CS 2530 
 * Assignment Generics
 * September 23, 2014
 * 
 */
namespace AssignmentGenerics
{
    class Program
    {
        static void Main(string[] args)
        {
            FindDuplicateWordsInSentence("The optimist thinks this is the best of all possible worlds; the pessimist fears it is true");

            ModifyList();

            Console.Title = "Sorting Generics Using Linq";

            Console.ReadKey();

        }

        // public static methods
        public static void FindDuplicateWordsInSentence(string sentence)
        {
            // make a dictionary to word each words and their frequencies
            Dictionary<string, int> words = new Dictionary<string, int>();

            AddWordsToDictionary(sentence, words);

            var shortest = getShortestWords(words);

            string shortestWords = String.Join(", ", shortest);

            PrintWords(sentence, words, shortestWords);

        }

        public static void ModifyList()
        {
            List<int> numbers = new List<int>();


            // fill up list with random numbers
            AddRandomNumsToList(numbers);

            // find the median and mode
            numbers.Sort();
            double median = FindMedian(numbers);
            var modeGroups = GetModeGroups(numbers);

            // print the numbers, mean, median, mode, and range
            PrintNumbers(numbers, median, modeGroups);

        }


        // private methods
        private static void AddWordsToDictionary(string sentence, Dictionary<string, int> words)
        {
            // split the string into each word token
            foreach (string w in Regex.Split(sentence, "\\W+"))
            {
                string word = w.ToLower();

                // if the word is in the dictionary, then increase its frequency
                if (words.ContainsKey(word))
                {
                    words[word]++;
                }
                // if word is not in the dictionary, then add it
                else
                {
                    words.Add(word, 1);
                }
            }
        }

        private static IEnumerable<string> getShortestWords(Dictionary<string, int> words)
        {
            var shortestLength = from word in words.Keys
                                 orderby word.Length
                                 select word;

            var shortestWords = from word in shortestLength
                                where word.Length == shortestLength.ElementAt(0).Length
                                select word;

            return shortestWords;
        }

        private static void PrintWords(string sentence, Dictionary<string, int> words, string shortest)
        {

            Console.WriteLine("\n{0}\n\n", sentence);

            foreach (KeyValuePair<string, int> kvp in words)
            {
                Console.WriteLine("{0, -10} {1}", kvp.Key, kvp.Value);
            }

            Console.WriteLine();


            Console.WriteLine("Shortest word(s): {0}", shortest);
        }

        private static void AddRandomNumsToList(List<int> numbers)
        {
            Random rand = new Random(17);

            for (int i = 0; i < 25; i++)
            {
                numbers.Add(rand.Next(20, 80));
            }
        }

        private static double FindMedian(List<int> numbers)
        {
            if (numbers.Count() == 0)
            {
                throw new ArgumentOutOfRangeException("List must contain at least one number");
            }

            double median;
            int mid = (numbers.Count - 1) / 2;  // -1 to account for the index of zero based array

            if (numbers.Count % 2 == 0)
            {

                median = (numbers[mid] + numbers[mid + 1]) / 2.0;
            }
            else
            {
                median = numbers[mid];
            }

            return median;
        }

        private static IEnumerable<int> GetModeGroups(List<int> numbers)
        {
            var groups = from n in numbers
                         group n by n;

            var modeGroups = from g in groups
                             where g.Count() > 1
                             orderby g.Count() descending
                             select g;

            var mode = from g in modeGroups
                       where g.Count() == modeGroups.ElementAt(0).Count()
                       select g;


            var keys = from g in mode
                       select g.Key;

            return keys;
        }

        private static void PrintNumbers(List<int> numbers, double median, IEnumerable<int> modeGroups)
        {

            Console.WriteLine("\n\n");
            Console.WriteLine("*".PadRight(80, '*'));
            Console.WriteLine("List of random numbers:\n\n{0}\n\n", String.Join(", ", numbers));

            Console.WriteLine("Mean: {0}", numbers.Average());
            Console.WriteLine("Median: {0}", median);
            Console.WriteLine("Mode: {0}", (modeGroups.Count() == 0) ? "none" : String.Join(", ", modeGroups));
            Console.WriteLine("Range: {0}", (numbers[numbers.Count - 1] - numbers[0]));
        }

    }
}
