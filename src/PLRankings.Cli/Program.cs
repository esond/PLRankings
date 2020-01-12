using System;
using System.IO;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using PLRankings.Data;
using PLRankings.Manager;

namespace PLRankings.Cli
{
    public class Program
    {
        private const string OutputDirectory = "C:\\temp\\rankings.rawdata";
        private const int CompetitionYear = 2019;

        public static async Task Main(string[] args)
        {
            var kernel = new StandardKernel(new ServiceModule());

            var rankingManager = kernel.Get<IRankingManager>();

            Console.WriteLine($"Creating rankings for year {CompetitionYear}...");

            var files = await rankingManager.ExportRankingsToCsvAsync(CompetitionYear);

            foreach (var (title, content) in files)
            {
                var fileName = $"{OutputDirectory}\\{title}";

                Console.WriteLine($"Saving {fileName}...");

                await File.WriteAllTextAsync(fileName, content);
            }

            Console.WriteLine($"Rankings saved to {OutputDirectory}");
        }
    }

    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRankingManager>().To<RankingManager>();
            Bind<ICompetitionDataAccess>().To<HtmlCompetitionDataAccess>();
        }
    }
}