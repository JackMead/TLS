using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            string pattern = @"\w\w\w";
            findTLS(text,pattern);

            

        }

        private void findTLS(string text, string pattern)
        {
            
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            Dictionary<string, int> tlsDictionary = new Dictionary<string, int> {};
            MatchCollection matches = rgx.Matches(text);

            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
                if (tlsDictionary.ContainsKey(match.Value))
                {
                    tlsDictionary[match.Value] += 1;
                }
                else
                {
                    tlsDictionary.Add(match.Value, 1);
                }
            }
        

            Console.WriteLine("The TLS tra appears " + tlsDictionary["tra"] + " times");
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
