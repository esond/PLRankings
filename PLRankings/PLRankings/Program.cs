using System;
using PLRankings.Features;

namespace PLRankings
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string openMenRaw2015 =
                "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
            const string openMenRaw2016 =
                "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";

            const string outputFile = "C:\\Users\\esond_000\\Desktop\\APU Rankings\\Men - Open (2015-2016).csv";

            DateTime provincials2015Date = new DateTime(2015, 7, 4);
            DateTime provincials2016Date = new DateTime(2016, 6, 11);

            Scraper.CreateRanking(outputFile, provincials2015Date, provincials2016Date, openMenRaw2015, openMenRaw2016);
        }
    }
}
