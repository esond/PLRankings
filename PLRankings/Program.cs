using System.Configuration;
using System.IO;
using PLRankings.Data;
using PLRankings.Features;

namespace PLRankings
{
    public class Program
    {
        private static readonly string Year = "2018";

        public static void Main(string[] args)
        {
            var path = ConfigurationManager.AppSettings["outputFilePath"];

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var outputFile = Path.Combine(path, $"Men - Open ({Year}).csv");

            RankingGenerator.CreateRanking(outputFile, ResultsUris.OpenMen2018);

            outputFile = Path.Combine(path, $"Women - Open ({Year}).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.OpenWomen2018);

            outputFile = Path.Combine(path, $"Men - Junior and Sub-Junior ({Year}).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.JuniorMen2018);

            outputFile = Path.Combine(path, $"Women - Junior and Sub-Junior ({Year}).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.JuniorWomen2018);

            outputFile = Path.Combine(path, $"Men - Masters ({Year}).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.MastersMen2018);

            outputFile = Path.Combine(path, $"Women - Masters ({Year}).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.MastersWomen2018);

            outputFile = Path.Combine(path, $"Men - Bench-Only ({Year}).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.BenchOnlyMen2018);

            outputFile = Path.Combine(path, $"Women - Bench-Only ({Year}).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.BenchOnlyWomen2018);

            outputFile = Path.Combine(path, $"Overall - Equipped ({Year}).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.EquippedOverall2018, cr => !cr.Unequipped);
        }
    }
}
