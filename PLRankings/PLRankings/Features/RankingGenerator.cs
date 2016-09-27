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
        public static void CreateRanking(string outputFile, DateTime seasonStartDate, DateTime seasonEndDate, IEnumerable<string> resultsUris)
        {
            Console.WriteLine($"Creating ranking {outputFile}...");

            IEnumerable<CompetitionResult> results = Scraper.GetResults(seasonStartDate, seasonEndDate, resultsUris);

            IEnumerable<CompetitionResult> orderedResults = results.OrderByDescending(cr => cr.WilksPoints);

            StringBuilder builder = new StringBuilder();

            foreach (var line in SerializationHelper.ToCsv(orderedResults))
            {
                builder.AppendLine(line);
            }

            File.WriteAllText(outputFile, builder.ToString());
        }
    }
}
