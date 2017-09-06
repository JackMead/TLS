using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace TLS
{
    class ThreeLetterSequencesAnalyser
    {
        public void Run()
        {
            string text = GetText();

            //Find all three-letter-sequences from the text
            var tlsDictionary = FindSimpleTLS(text);
            Console.WriteLine("The TLS \"tra\" appears " + tlsDictionary["tra"] + " times");
            PrintTLSForGivenFrequency(tlsDictionary, 63);
            PrintMostCommonSequences(tlsDictionary, 10);
            UserChoicePrintOccurences(tlsDictionary);

            //Find all three-letter-sequences from the text ignoring whitespace
            var ignoredSpaceTLSDictionary = FindTLSIgnoringWhitespace(text);
            PrintMostCommonSequences(ignoredSpaceTLSDictionary, 10);

            //Find all three-letter-sequences from a webpage.
            string url = @"https://raw.githubusercontent.com/CorndelWithSoftwire/ThreeLetterSequences/master/SampleText.txt";
            string webText = GetTextFromWeb(url);
            Dictionary<string, int> tlsDictionaryFromWeb = FindSimpleTLS(webText);
        }

        private Dictionary<string, int> FindSimpleTLS(string text)
        {
            string pattern = @"\w\w\w";
            return FindTLS(text, pattern);
        }

        private Dictionary<string, int> FindTLSIgnoringWhitespace(string text)
        {
            string pattern = @"((\w)\s*(\w)\s*(\w))";
            return FindTLS(text, pattern);
        }

        private Dictionary<string, int> FindTLS(string text, string pattern)
        {
            var tlsDictionary = new Dictionary<string, int>();
            var regex = new Regex(pattern);
            var match = regex.Match(text);
            while (match.Success)
            {
                string tlsLower = match.Value.ToLower();
                if (tlsDictionary.ContainsKey(tlsLower))
                {
                    tlsDictionary[tlsLower] += 1;
                }
                else
                {
                    tlsDictionary.Add(tlsLower, 1);
                }
                //Having found a match, look for the next match starting from the next character
                match = regex.Match(text, match.Index + 1);
            }
            return tlsDictionary;
        }

        private void UserChoicePrintOccurences(Dictionary<string, int> tlsDictionary)
        {
            Console.WriteLine("\nInput an integer, and we'll see which tls occur that often");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int userChoice))
            {
                PrintTLSForGivenFrequency(tlsDictionary, userChoice);
            }
            else
            {
                Console.WriteLine("Sorry, didn't understand that. Maybe next time");
            }
        }

        private void PrintMostCommonSequences(Dictionary<string, int> tlsDictionary, int numToPrint)
        {
            //Find the n largest number of occurrences
            var topTLS = tlsDictionary.OrderByDescending(p => p.Value).Take(numToPrint);

            Console.WriteLine("\nThe most commonly occurring three-letter-sequences are:");
            foreach (var s in topTLS)
            {
                Console.WriteLine(s.Key + " appears " + s.Value + " times");
            }
        }

        private void PrintTLSForGivenFrequency(Dictionary<string, int> tlsDictionary, int occurences)
        {

            var sequences = tlsDictionary.Where(p => p.Value == occurences);

            //See if there are any results
            if (sequences.Count() > 0)
            {
                Console.WriteLine("\nThe following three-letter-sequences occur " + occurences + " times each:");
            }
            else
            {
                Console.WriteLine("No TLS occurs " + occurences + " times.");
            }

            foreach (var tls in sequences)
            {
                Console.WriteLine(tls.Key);
            }
        }
        
        private string GetTextFromWeb(string url)
        {
            WebClient client = new WebClient();
            Stream data = client.OpenRead(url);
            string webtext;
            using (StreamReader reader = new StreamReader(data)) { webtext = reader.ReadToEnd(); }
            data.Close();

            return webtext;
        }

        private string GetText()
        {
            var path = "SampleText.txt";

            return File.ReadAllText(path);
        }
    }
}
