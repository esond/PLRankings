using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using PLRankings.Data;
using PLRankings.Data.Contracts;

namespace PLRankings.Manager
{
    public class RankingManager : IRankingManager
    {
        private readonly ICompetitionDataAccess _competitionData;

        public RankingManager(ICompetitionDataAccess competitionData)
        {
            _competitionData = competitionData;
        }

        public async Task<IEnumerable<(string title, string content)>> ExportRankingsToCsvAsync(int year)
        {
            var results = new List<(string title, string content)>();

            var result = await _competitionData.GetCompetitionResultsAsync(new CompetitionDataRequest
            {
                CompetitionType = "All",
                Gender = "M",
                Province = "AB",
                AgeCategory = "Open",
                Year = year,
                Unequipped = true
            });

            using (var stringWriter = new StringWriter())
            using (var csvWriter = new CsvWriter(stringWriter))
            {
                csvWriter.WriteRecords(result);

                results.Add(($"Men - Open ({year}).csv", stringWriter.ToString()));
            }

            return results;
        }
    }
}
