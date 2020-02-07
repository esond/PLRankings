using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PLRankings.Models;

namespace PLRankings.Resource
{
    public class HtmlCompetitionDatabase : ICompetitionDatabase
    {
        //TODO: move to config
        private const string CpuLifterDatabaseUrl =
            "http://www.powerlifting.ca/cpu/index.php/competitors/lifter-database";

        private readonly HttpClient _httpClient;

        public HtmlCompetitionDatabase(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #region Implementation of ICompetitionDatabase

        public async Task<IEnumerable<CompetitionResult>> QueryAsync(CompetitionResultQuery query)
        {
            using var response = await _httpClient.SendAsync(CreateHttpRequest(query));

            response.EnsureSuccessStatusCode();

            var html = await response.Content.ReadAsStringAsync();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var resultsTableNode = htmlDocument.DocumentNode.SelectNodes("//form/table")
                .SingleOrDefault(n => n.Id == "lifter_database");

            if (resultsTableNode == null)
                return new CompetitionResult[]{};

            var resultsRowNodes = resultsTableNode.ChildNodes.Skip(2).ToList();

            double ParseDoubleOrDefault(string value)
            {
                return double.TryParse(value, out var result) ? result : default;
            }

            var results = resultsRowNodes.Select(rrn => new CompetitionResult
            {
                ContestName = rrn.ChildNodes[0].InnerText,
                Date = DateTime.Parse(rrn.ChildNodes[1].InnerText),
                Location = rrn.ChildNodes[2].InnerText,
                ContestType = rrn.ChildNodes[3].InnerText,
                Gender = rrn.ChildNodes[4].InnerText,
                AthleteName = rrn.ChildNodes[5].InnerText,
                Province = rrn.ChildNodes[6].InnerText,
                Bodyweight = ParseDoubleOrDefault(rrn.ChildNodes[7].InnerText),
                WeightClass = rrn.ChildNodes[8].InnerText,
                AgeCategory = rrn.ChildNodes[9].InnerText,
                Squat = ParseDoubleOrDefault(rrn.ChildNodes[10].InnerText),
                Bench = ParseDoubleOrDefault(rrn.ChildNodes[11].InnerText),
                Deadlift = ParseDoubleOrDefault(rrn.ChildNodes[12].InnerText),
                Total = ParseDoubleOrDefault(rrn.ChildNodes[13].InnerText),
                Points = ParseDoubleOrDefault(rrn.ChildNodes[14].InnerText),
                Unequipped = rrn.ChildNodes[15].InnerText == "yes"
            }).ToList();

            return results.ToArray();
        }

        #endregion

        private static HttpRequestMessage CreateHttpRequest(CompetitionResultQuery resultQuery)
        {
            return new HttpRequestMessage(HttpMethod.Post, CpuLifterDatabaseUrl)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "style", resultQuery.CompetitionType },
                    { "gender", resultQuery.Gender },
                    { "province", resultQuery.Province },
                    { "age_category", resultQuery.AgeCategory },
                    { "weightclass_new", resultQuery.WeightClass },
                    { "year", resultQuery.Year.HasValue ? resultQuery.Year.ToString() : null },
                    { "name", resultQuery.AthleteName },
                    { "unequipped", resultQuery.Unequipped.HasValue
                        ? resultQuery.Unequipped.Value ? "yes" : "no"
                        : null },
                    { "contest", resultQuery.ContestName },
                    { "submit", "Search" } // Required to actually execute a search for some reason
                })
            };
        }
    }
}
