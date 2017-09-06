using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TLS
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.run();
        }

        private void run()
        {
            string text = getText();

            //Find all three-letter-sequences from the text
            string pattern = @"\w\w\w";
            Dictionary<string, int> tlsDictionary = findTLS(text, pattern);
            findByOccurences(tlsDictionary, 63);
            printMostCommonSequences(tlsDictionary, 10);
            userInput(tlsDictionary);

            //Find all three-letter-sequences from the text ignoring whitespace
            string altPattern = @"((\w)\s?(\w)\s?(\w))";
            Dictionary<string, int> ignoredSpaceTLSDictionary = findTLS(text, altPattern);
            printMostCommonSequences(ignoredSpaceTLSDictionary, 10);

            //Repeats the first steps of above using (here the same) text found online
            tlsFromWeb();
        }

        private void tlsFromWeb()
        {
            //TODO
            string url = @"https://raw.githubusercontent.com/CorndelWithSoftwire/ThreeLetterSequences/master/SampleText.txt";
            //Overwritten for checking
            //url = @"https://corndeltraining.softwire.com/2CWM-Bootcamp/03-ThreeLetterSequences/LearningGuide.html";

            WebClient client = new WebClient();

            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();

            string pattern = @"\w\w\w";
            Console.WriteLine(s);
            Dictionary<string, int> tlsDictionary = findTLS(s, pattern);

        }

        private void userInput(Dictionary<string, int> tlsDictionary)
        {
            Console.WriteLine("Input an integer, and we'll see which tls occur that often");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                findByOccurences(tlsDictionary, result);
            }
            else
            {
                Console.WriteLine("Sorry, didn't understand that. Maybe next time");
            }
        }

        private void printMostCommonSequences(Dictionary<string, int> tlsDictionary, int numToPrint)
        {
            //Find the 10 largest number of occurrences
            var topTen = tlsDictionary.OrderByDescending(p => p.Value).Take(10);

            foreach (KeyValuePair<string, int> s in topTen)
            {
                Console.WriteLine(s.Key + " appears " + s.Value + " times");
            }

            Console.WriteLine("There are a total of " + tlsDictionary.Values.Sum() + " three letter sequences");
        }

        private void findByOccurences(Dictionary<string, int> tlsDictionary, int occurences)
        {

            var sequences = tlsDictionary.Where(p => tlsDictionary[p.Key] == occurences);

            foreach (var tls in sequences)
            {
                Console.WriteLine(tls.Key + " occurs " + occurences + " times");
            }

            //Output if there are no such results
            if (sequences.Count() == 0)
            {
                Console.WriteLine("No TLS occurs " + occurences + " times.");
            }
        }

        private Dictionary<string, int> findTLS(string text, string pattern)
        {


            Dictionary<string, int> tlsDictionary = new Dictionary<string, int> { };

            Regex regexObj = new Regex(pattern);
            Match matchObj = regexObj.Match(text);
            while (matchObj.Success)
            {
                string tlsLower = matchObj.Value.ToLower();
                if (tlsDictionary.ContainsKey(tlsLower))
                {
                    tlsDictionary[tlsLower] += 1;
                }
                else
                {
                    tlsDictionary.Add(tlsLower, 1);
                }
                matchObj = regexObj.Match(text, matchObj.Index + 1);
            }

            Console.WriteLine("The TLS tra appears " + tlsDictionary["tra"] + " times");

            return tlsDictionary;
        }

        private string getText()
        {
            string path = "SampleText.txt";

            // Open the file to read from.
            string readText = File.ReadAllText(path);
            return readText;
        }
    }
}
