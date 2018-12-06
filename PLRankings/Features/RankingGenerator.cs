using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PLRankings.Models;

namespace PLRankings.Features
{
    public static class RankingGenerator
    {
        public static void CreateRanking(string outputFile, IEnumerable<string> resultsUris,
            Func<CompetitionResult, bool> additionalPredicate = null)
        {
            Console.WriteLine($"Creating ranking {outputFile}...");

            var results = Scraper.GetResults(resultsUris);

            var builder = new StringBuilder();

            if (additionalPredicate == null)
                additionalPredicate = x => true;

            foreach (var line in SerializationHelper.ToCsv(results.Where(additionalPredicate).OrderByDescending(r => r.WilksPoints)))
            {
                builder.AppendLine(line);
            }

            File.WriteAllText(outputFile, builder.ToString());
        }
    }
}
