using System;
using System.Configuration;
using System.Threading.Tasks;
using Ninject;

namespace PLRankings.Cli
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var kernel = new StandardKernel(new ServiceModule());

            var outputDirectory = ConfigurationManager.AppSettings["OutputDirectory"];
            var competitionYear = int.Parse(ConfigurationManager.AppSettings["CompetitionYear"]);
            var province = ConfigurationManager.AppSettings["Province"];

            await kernel.Get<RankingGenerator>().RunAsync(competitionYear, province, outputDirectory);

            var originalForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done!");
            Console.ForegroundColor = originalForeground;
        }
    }
}