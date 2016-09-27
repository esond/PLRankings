using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

            foreach (var line in ToCsv(orderedResults))
            {
                builder.AppendLine(line);
            }

            File.WriteAllText(outputFile, builder.ToString());
        }

        private static IEnumerable<string> ToCsv<T>(IEnumerable<T> objectlist, string separator = ",", bool header = true)
        {
            FieldInfo[] fields = typeof(T).GetFields();
            PropertyInfo[] properties = typeof(T).GetProperties();

            if (header)
            {
                yield return string.Join(separator, fields.Select(f => f.Name.ToDisplayText())
                    .Concat(properties.Select(p => p.Name.ToDisplayText())).ToArray());
            }

            foreach (T obj in objectlist)
            {
                yield return string.Join(separator, fields.Select(f => (f.GetValue(obj) ?? "").ToString())
                    .Concat(properties.Select(p => (p.GetValue(obj, null) ?? "").ToString())).ToArray());
            }
        }

        private static string ToDisplayText(this string str)
        {
            return Regex.Replace(str, @"([a-z](?=[A-Z0-9])|[A-Z](?=[A-Z][a-z]))", "$1 ");
        }
    }
}
