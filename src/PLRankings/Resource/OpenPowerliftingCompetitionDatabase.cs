using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using PLRankings.Models;

namespace PLRankings.Resource
{
    public class OpenPowerliftingCompetitionDatabase : ICompetitionDatabase
    {
        //todo: move to config
        private const string CpuDataFolder = "C:\\repos\\oss\\opl-data\\meet-data\\cpu";

        private const string IpfDataFolder = "C:\\repos\\oss\\opl-data\\meet-data\\ipf";

        public Task<IEnumerable<CompetitionResult>> QueryAsync(CompetitionResultQuery query)
        {
            var dataDirectoryPaths = Directory.EnumerateDirectories(CpuDataFolder, $"{query.Year}*");

            var competitionResults = new List<CompetitionResult>();

            foreach (var dataDirectoryPath in dataDirectoryPaths)
            {
                var entriesPath = $"{dataDirectoryPath}\\entries.csv";

                if (!File.Exists(entriesPath))
                    continue;

                string contestName;
                DateTime contestDate;
                string contestProvince;
                string contestCity;

                using (var sr = new StreamReader($"{dataDirectoryPath}\\meet.csv"))
                using (var csv = new CsvReader(sr, CultureInfo.InvariantCulture))
                {
                    csv.Read(); // Advance to headers (though the doc for this method says it doesn't read headers)
                    csv.Read(); // Advance to first data row
                    
                    contestName = csv[5];
                    contestDate = DateTime.Parse(csv[1]);
                    contestProvince = csv[3];
                    contestCity = csv[4];
                }

                using (var sr = new StreamReader($"{dataDirectoryPath}\\entries.csv"))
                using (var csv = new CsvReader(sr, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null
                }))
                {
                    var entries = csv.GetRecords<OpenPowerliftingEntry>();

                    competitionResults.AddRange(entries.Select(e =>
                    {
                        var equipment = e.Equipment == "Raw" ? Equipment.Raw : Equipment.SinglePly;
                        var competitionType = e.Event == "SBD"
                            ? CompetitionType.ThreeLift
                            : CompetitionType.BenchOnly;
                        var sex = e.Sex == "M" ? Sex.Male : Sex.Female;
                        return new CompetitionResult
                        {
                            ContestName = contestName,
                            Date = contestDate,
                            Location = $"{contestCity}, {contestProvince}",
                            CompetitionType = competitionType,
                            Sex = sex,
                            AthleteName = AliasDictionary.Aliases.TryGetValue(e.Name, out var realName)
                                ? realName
                                : e.Name,
                            Province = e.State,
                            Bodyweight = e.BodyweightKg,
                            WeightClass = e.WeightClassKg,
                            AgeCategory = e.Division,
                            Squat = e.Best3SquatKg.GetValueOrDefault(),
                            Bench = e.Best3BenchKg.GetValueOrDefault(),
                            Deadlift = e.Best3DeadliftKg.GetValueOrDefault(),
                            Total = e.TotalKg.GetValueOrDefault(),
                            Points = PointsCalculator.CalculateIpfPoints(sex, equipment, competitionType, e.BodyweightKg, e.TotalKg.GetValueOrDefault()),
                            Equipment = equipment
                        };
                    }));
                }
            }

            var resultsEnumerable = competitionResults
                .OrderByDescending(r => r.Points)
                .Where(r => r.Province == query.Province)
                .Where(r => r.Sex == query.Sex)
                .Where(r => r.CompetitionType == query.CompetitionType);

            if (!string.IsNullOrEmpty(query.AgeCategory))
                resultsEnumerable = resultsEnumerable.Where(r => r.AgeCategory == query.AgeCategory);

            if (!string.IsNullOrEmpty(query.WeightClass))
                resultsEnumerable = resultsEnumerable.Where(r => r.WeightClass == query.WeightClass);

            if (!string.IsNullOrEmpty(query.AthleteName))
            {
                resultsEnumerable = resultsEnumerable.Where(r =>
                    string.Equals(r.AthleteName, query.AthleteName, StringComparison.CurrentCultureIgnoreCase));
            }
            
            if (!string.IsNullOrEmpty(query.ContestName))
            {
                resultsEnumerable = resultsEnumerable.Where(r =>
                    string.Equals(r.ContestName, query.ContestName, StringComparison.CurrentCultureIgnoreCase));
            }

            if (query.Equipment != null)
                resultsEnumerable = resultsEnumerable.Where(r => r.Equipment == query.Equipment);

            return Task.FromResult(resultsEnumerable);
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        private class OpenPowerliftingEntry
        {
            public string Event { get; set; }

            public string Sex { get; set; }

            public string Name { get; set; }

            public string State { get; set; }

            public double BodyweightKg { get; set; }

            public string WeightClassKg { get; set; }

            public string Division { get; set; }

            public double? Best3SquatKg { get; set; }

            public double? Best3BenchKg { get; set; }

            public double? Best3DeadliftKg { get; set; }

            public double? TotalKg { get; set; }

            public string Equipment { get; set; }
        }
    }
}
