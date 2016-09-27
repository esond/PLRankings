using System;
using System.Collections.Generic;
using System.IO;
using PLRankings.Features;

namespace PLRankings
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DateTime provincials2015Date = new DateTime(2015, 7, 4);
            DateTime provincials2016Date = new DateTime(2016, 6, 11);

            string outputFile = GetOutputFilePath("Men - Open (2015-2016).csv");
            RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, OpenMenUris);

            outputFile = GetOutputFilePath("Women - Open (2015-2016).csv");
            RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, OpenWomenUris);

            outputFile = GetOutputFilePath("Men - Junior and Sub-Junior (2015-2016).csv");
            RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, JuniorMenUris);

            outputFile = GetOutputFilePath("Women - Junior and Sub-Junior (2015-2016).csv");
            RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, JuniorWomenUris);

            outputFile = GetOutputFilePath("Men - Masters (2015-2016).csv");
            RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, MastersMenUris);

            outputFile = GetOutputFilePath("Women - Masters (2015-2016).csv");
            RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, MastersWomenUris);

            // TODO: Equipped and Bench-Only
            //outputFile = GetOutputFilePath("Men - Equipped (2015-2016).csv");
            //RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, EquippedMenUris);

            //outputFile = GetOutputFilePath("Women - Equipped (2015-2016).csv");
            //RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, EquippedWomenUris);
        }

        private static IEnumerable<string> OpenMenUris
        {
            get
            {
                yield return //2015
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=M&Prov.=AB&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
            }
        }

        private static IEnumerable<string> OpenWomenUris
        {
            get
            {
                yield return //2015
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Open&BW%2FCl.+=&BW-CL-NEW=&Year=2015&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
                yield return //2016
                    "http://www.powerlifting.ca/cgi-bin/webdata_cpudb.pl?3+Lift%2FSingle=All&M+%2F+F=F&Prov.=AB&Age+Category=Open&BW%2FCl.+=&BW-CL-NEW=&Year=2016&NAME=&Uneq%3F=y&Contest=&cgifunction=Search&pagenum=1&cgisort=17&cgisortorder=1";
            }
        }

        private static IEnumerable<string> JuniorMenUris
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

        private static IEnumerable<string> JuniorWomenUris
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

        private static IEnumerable<string> MastersMenUris
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

        private static IEnumerable<string> MastersWomenUris
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

        private static string GetOutputFilePath(string fileName)
        {
            return Path.Combine("C:\\Users\\esond_000\\Desktop\\APU Rankings", fileName);
        }
    }
}
