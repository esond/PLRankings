using System;
using System.Threading.Tasks;
using Ninject;

namespace PLRankings.Cli
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var kernel = new StandardKernel(new ServiceModule());

            await kernel.Get<RankingGenerator>().RunAsync();

            var originalForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done!");
            Console.ForegroundColor = originalForeground;
        }
    }
}