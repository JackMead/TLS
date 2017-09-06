using System;
using System.Threading.Tasks;

namespace TLS
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new ThreeLetterSequencesAnalyser();
            p.Run();

            var r = new RegexExperiment();
            r.Run();
        }
    }
}
