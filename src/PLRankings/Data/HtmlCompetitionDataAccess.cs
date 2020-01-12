using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PLRankings.Data.Contracts;
using PLRankings.Models;

namespace PLRankings.Data
{
    public class HtmlCompetitionDataAccess : ICompetitionDataAccess
    {
        //TODO: move to config
        private const string CpuLifterDatabaseUrl =
            "http://www.powerlifting.ca/cpu/index.php/competitors/lifter-database";

        private readonly HttpClient _httpClient;

        public HtmlCompetitionDataAccess(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CompetitionResult>> GetCompetitionResultsAsync(CompetitionDataRequest dataRequest)
        {
            using var response = await _httpClient.SendAsync(CreateHttpRequest(dataRequest));

            response.EnsureSuccessStatusCode();

            var html = await response.Content.ReadAsStringAsync();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var resultsTableNode = htmlDocument.DocumentNode.SelectNodes("//form/table")
                .Single(n => n.Id == "lifter_database");

            var resultsRowNodes = resultsTableNode.ChildNodes.Skip(2).ToList();

            var results = resultsRowNodes.Select(rrn => new CompetitionResult
            {
                ContestName = rrn.ChildNodes[0].InnerText,
                Date = DateTime.Parse(rrn.ChildNodes[1].InnerText),
                Location = rrn.ChildNodes[2].InnerText,
                ContestType = rrn.ChildNodes[3].InnerText,
                Gender = rrn.ChildNodes[4].InnerText,
                AthleteName = rrn.ChildNodes[5].InnerText,
                Province = rrn.ChildNodes[6].InnerText,
                Bodyweight = double.Parse(rrn.ChildNodes[7].InnerText),
                WeightClass = rrn.ChildNodes[8].InnerText,
                AgeCategory = rrn.ChildNodes[9].InnerText,
                Squat = double.Parse(rrn.ChildNodes[10].InnerText),
                Bench = double.Parse(rrn.ChildNodes[11].InnerText),
                Deadlift = double.Parse(rrn.ChildNodes[12].InnerText),
                Total = double.Parse(rrn.ChildNodes[13].InnerText),
                Points = double.Parse(rrn.ChildNodes[14].InnerText),
                Unequipped = rrn.ChildNodes[15].InnerText == "yes"
            }).ToList();

            //TODO: Dedupe based on name

            return results.OrderByDescending(cr => cr.Points);
        }

        private HttpRequestMessage CreateHttpRequest(CompetitionDataRequest dataRequest)
        {
            var formData = new Dictionary<string, string>
            {
                { "style", dataRequest.CompetitionType },
                { "gender", dataRequest.Gender },
                { "province", dataRequest.Province },
                { "age_category", dataRequest.AgeCategory },
                { "weightclass_new", dataRequest.WeightClass },
                { "year", dataRequest.Year.ToString() },
                { "name", dataRequest.AthleteName },
                { "unequipped", dataRequest.Unequipped.HasValue
                    ? dataRequest.Unequipped.Value ? "yes" : "no"
                    : null },
                { "contest", dataRequest.ContestName },
                { "submit", "Search" } // Required to produce search results, for some reason
            };

            return new HttpRequestMessage(HttpMethod.Post, CpuLifterDatabaseUrl)
            {
                Content = new FormUrlEncodedContent(formData)
            };
        }
    }
}
