using System.IO;
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

            outputFile = GetOutputFilePath("Men - Junior and Sub-Junior (22016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.JuniorMen2016);

            outputFile = GetOutputFilePath("Women - Junior and Sub-Junior (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.JuniorWomen2016);

            outputFile = GetOutputFilePath("Men - Masters (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.MastersMen2016);

            outputFile = GetOutputFilePath("Women - Masters (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.MastersWomen2016);

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
