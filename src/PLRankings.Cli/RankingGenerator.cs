using System;
using System.IO;
using System.Threading.Tasks;
using PLRankings.Manager;

namespace PLRankings.Cli
{
    public class RankingGenerator
    {

        private readonly IRankingManager _rankingManager;

        public RankingGenerator(IRankingManager rankingManager)
        {
            _rankingManager = rankingManager;
        }

        public async Task RunAsync(int competitionYear, string province, string outputDirectory)
        {
            Console.WriteLine($"Creating rankings for year {competitionYear}...");

            var files = await _rankingManager.ExportRankingsToCsvAsync(competitionYear, province);

            foreach (var (title, content) in files)
            {
                var fileName = $"{outputDirectory}\\{title}";

                Console.WriteLine($"Saving {fileName}...");

                await File.WriteAllTextAsync(fileName, content);
            }

            Console.WriteLine($"Rankings saved to {outputDirectory}");
        }
    }
}
