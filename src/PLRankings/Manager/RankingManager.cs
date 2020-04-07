using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using PLRankings.Access;

namespace PLRankings.Manager
{
    public class RankingManager : IRankingManager
    {
        private readonly ICompetitionDataAccess _competitionData;

        public RankingManager(ICompetitionDataAccess competitionData)
        {
            _competitionData = competitionData;
        }

        public async Task<IEnumerable<(string title, string content)>> ExportRankingsToCsvAsync(int year, string province)
        {
            var resultsByName = new Dictionary<string, string>();

            var menOpenData = await _competitionData.GetMenOpenResultsAsync(year, province);
            resultsByName.Add($"Men - Open ({year}).csv", GetCsvString(menOpenData));

            var menJuniorSubJuniorData = await _competitionData.GetMenJuniorAndSubJuniorResultsAsync(year, province);
            resultsByName.Add($"Men - Junior and Sub-Junior ({year}).csv", GetCsvString(menJuniorSubJuniorData));

            var menMasterData = await _competitionData.GetMenMasterResultsAsync(year, province);
            resultsByName.Add($"Men - Master ({year}).csv", GetCsvString(menMasterData));

            var menBenchOnlyData = await _competitionData.GetMenBenchOnlyResultsAsync(year, province);
            resultsByName.Add($"Men - Bench-Only ({year}).csv", GetCsvString(menBenchOnlyData));

            var womenOpenData = await _competitionData.GetWomenOpenResultsAsync(year, province);
            resultsByName.Add($"Women - Open ({year}).csv", GetCsvString(womenOpenData));

            var womenJuniorSubJuniorData = await _competitionData.GetWomenJuniorAndSubJuniorResultsAsync(year, province);
            resultsByName.Add($"Women - Junior and Sub-Junior ({year}).csv", GetCsvString(womenJuniorSubJuniorData));

            var womenMasterData = await _competitionData.GetWomenMasterResultsAsync(year, province);
            resultsByName.Add($"Women - Master ({year}).csv", GetCsvString(womenMasterData));

            var womenBenchOnlyData = await _competitionData.GetWomenBenchOnlyResultsAsync(year, province);
            resultsByName.Add($"Women - Bench-Only ({year}).csv", GetCsvString(womenBenchOnlyData));

            var overallEquippedData = await _competitionData.GetOverallEquippedResultsAsync(year, province);
            resultsByName.Add($"Overall - Equipped ({year}).csv", GetCsvString(overallEquippedData));

            return resultsByName.Select(kvp => (kvp.Key, kvp.Value));
        }

        private static string GetCsvString<T>(IEnumerable<T> records)
        {
            using var stringWriter = new StringWriter();
            using var csvWriter = new CsvWriter(stringWriter, CultureInfo.CurrentCulture);

            csvWriter.WriteRecords(records);

            return stringWriter.ToString();
        }
    }
}
