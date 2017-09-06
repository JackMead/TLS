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
            Dictionary<string, int> tlsDictionary = findTLS(text,pattern);

            Console.WriteLine("The TLS pre appears: " + tlsDictionary["pre"] + " times");

            findByOccurences(tlsDictionary, 63);

        }

        private void findByOccurences(Dictionary<string, int> tlsDictionary, int occurences)
        {
            foreach(string tls in tlsDictionary.Keys)
            {
                if (tlsDictionary[tls] == occurences)
                {
                    Console.WriteLine(tls + " also occurs " + occurences + " times");
                }
            }
        }

        private Dictionary<string,int> findTLS(string text, string pattern)
        {
            
            
            Dictionary<string, int> tlsDictionary = new Dictionary<string, int> {};
                        
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
