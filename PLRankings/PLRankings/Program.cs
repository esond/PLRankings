using System;
using System.IO;
using PLRankings.Data;
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
            RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, ResultsUris.OpenMenUris);

            outputFile = GetOutputFilePath("Women - Open (2015-2016).csv");
            RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, ResultsUris.OpenWomenUris);

            outputFile = GetOutputFilePath("Men - Junior and Sub-Junior (2015-2016).csv");
            RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, ResultsUris.JuniorMenUris);

            outputFile = GetOutputFilePath("Women - Junior and Sub-Junior (2015-2016).csv");
            RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, ResultsUris.JuniorWomenUris);

            outputFile = GetOutputFilePath("Men - Masters (2015-2016).csv");
            RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, ResultsUris.MastersMenUris);

            outputFile = GetOutputFilePath("Women - Masters (2015-2016).csv");
            RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, ResultsUris.MastersWomenUris);

            // TODO: Equipped and Bench-Only
            //outputFile = GetOutputFilePath("Men - Equipped (2015-2016).csv");
            //RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, EquippedMenUris);

            //outputFile = GetOutputFilePath("Women - Equipped (2015-2016).csv");
            //RankingGenerator.CreateRanking(outputFile, provincials2015Date, provincials2016Date, EquippedWomenUris);
        }

        private static string GetOutputFilePath(string fileName)
        {
            return Path.Combine("C:\\Users\\esond_000\\Desktop\\APU Rankings", fileName);
        }
    }
}
