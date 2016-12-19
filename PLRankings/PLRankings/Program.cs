﻿using System.Configuration;
using System.IO;
using System.Web.Configuration;
using PLRankings.Data;
using PLRankings.Features;

namespace PLRankings
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string outputFile = GetOutputFilePath("Men - Open (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.OpenMen2016);

            outputFile = GetOutputFilePath("Women - Open (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.OpenWomen2016);

            outputFile = GetOutputFilePath("Men - Junior and Sub-Junior (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.JuniorMen2016);

            outputFile = GetOutputFilePath("Women - Junior and Sub-Junior (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.JuniorWomen2016);

            outputFile = GetOutputFilePath("Men - Masters (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.MastersMen2016);

            outputFile = GetOutputFilePath("Women - Masters (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.MastersWomen2016);

            outputFile = GetOutputFilePath("Men - Bench-Only (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.BenchOnlyMen2016);

            outputFile = GetOutputFilePath("Women - Bench-Only (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.BenchOnlyWomen2016);

            outputFile = GetOutputFilePath("Overall - Equipped (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.EquippedOverall2016, cr => !cr.Unequipped);
        }

        private static string GetOutputFilePath(string fileName)
        {
            return Path.Combine(ConfigurationManager.AppSettings["outputFilePath"], fileName);
        }
    }
}
