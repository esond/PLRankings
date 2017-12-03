using System.Configuration;
using System.IO;
using PLRankings.Data;
using PLRankings.Features;

namespace PLRankings
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = ConfigurationManager.AppSettings["outputFilePath"];

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var outputFile = Path.Combine(path, "Men - Open (2016).csv");

            RankingGenerator.CreateRanking(outputFile, ResultsUris.OpenMen2016);

            outputFile = Path.Combine(path, "Women - Open (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.OpenWomen2016);

            outputFile = Path.Combine(path, "Men - Junior and Sub-Junior (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.JuniorMen2016);

            outputFile = Path.Combine(path, "Women - Junior and Sub-Junior (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.JuniorWomen2016);

            outputFile = Path.Combine(path, "Men - Masters (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.MastersMen2016);

            outputFile = Path.Combine(path, "Women - Masters (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.MastersWomen2016);

            outputFile = Path.Combine(path, "Men - Bench-Only (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.BenchOnlyMen2016);

            outputFile = Path.Combine(path, "Women - Bench-Only (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.BenchOnlyWomen2016);

            outputFile = Path.Combine(path, "Overall - Equipped (2016).csv");
            RankingGenerator.CreateRanking(outputFile, ResultsUris.EquippedOverall2016, cr => !cr.Unequipped);
        }
    }
}
