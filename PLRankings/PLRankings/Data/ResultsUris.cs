using System.Collections.Generic;

namespace PLRankings.Data
{
    public static class ResultsUris
    {
        public static IEnumerable<string> OpenMenUris
        {
            get
            {
                yield return //2015
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
            }
        }

        public static IEnumerable<string> OpenWomenUris
        {
            get
            {
                yield return //2015
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Open&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Open&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
            }
        }

        public static IEnumerable<string> JuniorMenUris
        {
            get
            {
                yield return //2015 Junior
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&Age+Category=Junior&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016 Junior
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&Age+Category=Junior&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2015 Sub-Junior
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&Age+Category=Sub-Junior&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016 Sub-Junior
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&Age+Category=Sub-Junior&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
            }
        }

        public static IEnumerable<string> JuniorWomenUris
        {
            get
            {
                yield return //2015 Junior
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Junior&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016 Junior
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Junior&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2015 Sub-Junior
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Sub-Junior&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016 Sub-Junior
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Sub-Junior&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
            }
        }

        public static IEnumerable<string> MastersMenUris
        {
            get
            {
                yield return //2015 M1
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&Age+Category=Master+1&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016 M1
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&Age+Category=Master+1&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2015 M2
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&Age+Category=Master+2&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016 M2
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&Age+Category=Master+2&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2015 M3
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&Age+Category=Master+3&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016 M3
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&Age+Category=Master+3&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2015 M4
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&Age+Category=Master+4&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016 M4
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&Age+Category=Master+4&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
            }
        }

        public static IEnumerable<string> MastersWomenUris
        {
            get
            {
                yield return //2015 M1
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Master+1&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016 M1
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Master+1&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2015 M2
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Master+2&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016 M2
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Master+2&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2015 M3
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Master+3&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016 M3
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Master+3&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2015 M4
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Master+4&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016 M4
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Master+4&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
            }
        }
    }
}
