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
    class RegexExperiment
    {
        public void Run()
        {
            string namesUrl = "https://corndeltraining.softwire.com/2CWM-Bootcamp/03-ThreeLetterSequences/LearningGuide.html";
            PrintNamesFromWebpage(namesUrl);

            string emailUrl = "https://www.softwire.com/contact/";
            PrintEmailsFromWebpage(emailUrl);
            
        }

        private void PrintEmailsFromWebpage(string url)
        {
            string pattern = @"[\w\.]*@\w+\.(co\.uk|com)";
            PrintRegexMatchesFromWebpage(pattern, url);
        }

        private void PrintNamesFromWebpage(string url)
        {
            string pattern = @"((Dr|Mr|Miss|Mrs)\s*(\w+)\s*(\w+))";
            PrintRegexMatchesFromWebpage(pattern, url);
        }

        private void PrintRegexMatchesFromWebpage(string pattern, string url)
        {
            string textToSearch = GetTextFromWeb(url);
            var regex = new Regex(pattern);
            var matches = regex.Matches(textToSearch);

            foreach (var match in matches)
            {
                Console.WriteLine(match);
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
    }
}
