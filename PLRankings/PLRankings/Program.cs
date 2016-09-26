
using PLRankings.Features;

namespace PLRankings
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string openMenRaw2015 =
                "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&Age+Category=Open&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
            const string openMenRaw2016 =
                "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&Age+Category=Open&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";

            const string outputFolder = "C:\\Users\\esond_000\\Desktop\\";

            Scraper scraper = new Scraper(outputFolder, openMenRaw2015, openMenRaw2016);
            scraper.GetData();
        }
    }
}
