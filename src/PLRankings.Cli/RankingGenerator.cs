using System;
using System.IO;
using System.Threading.Tasks;
using PLRankings.Manager;

namespace PLRankings.Cli
{
    public class RankingGenerator
    {
        private const string OutputDirectory = "C:\\temp\\rankings.rawdata";
        private const int CompetitionYear = 2019;

        private readonly IRankingManager _rankingManager;

        public RankingGenerator(IRankingManager rankingManager)
        {
            _rankingManager = rankingManager;
        }

        public async Task RunAsync()
        {
            Console.WriteLine($"Creating rankings for year {CompetitionYear}...");

            var files = await _rankingManager.ExportRankingsToCsvAsync(CompetitionYear, "AB");

            foreach (var (title, content) in files)
            {
                var fileName = $"{OutputDirectory}\\{title}";

                Console.WriteLine($"Saving {fileName}...");

                await File.WriteAllTextAsync(fileName, content);
            }

            Console.WriteLine($"Rankings saved to {OutputDirectory}");
        }
    }
}
