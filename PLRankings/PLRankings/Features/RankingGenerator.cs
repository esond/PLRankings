using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Flamtap.Serialization;
using PLRankings.Models;

namespace PLRankings.Features
{
    public static class RankingGenerator
    {
        public static void CreateRanking(string outputFile, IEnumerable<string> resultsUris)
        {
            Console.WriteLine($"Creating ranking {outputFile}...");

            IEnumerable<CompetitionResult> results = Scraper.GetResults(resultsUris);

            StringBuilder builder = new StringBuilder();

            foreach (string line in SerializationHelper.ToCsv(results.OrderByDescending(r => r.WilksPoints)))
            {
                builder.AppendLine(line);
            }

            File.WriteAllText(outputFile, builder.ToString());
        }
    }
}
