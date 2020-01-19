using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
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

        #region Implementation of ICompetitionDataAccess

        public async Task<IEnumerable<CompetitionResult>> GetCompetitionResultsAsync(CompetitionDataRequest dataRequest)
        {
            using var response = await _httpClient.SendAsync(CreateHttpRequest(dataRequest));

            response.EnsureSuccessStatusCode();

            var html = await response.Content.ReadAsStringAsync();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var resultsTableNode = htmlDocument.DocumentNode.SelectNodes("//form/table")
                .SingleOrDefault(n => n.Id == "lifter_database");

            if (resultsTableNode == null)
                return Enumerable.Empty<CompetitionResult>();

            //TODO: Validate number of results against "search resulted in" message

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

            return SelectTopResultPerLifter(results)
                .OrderByDescending(cr => cr.Points);
        }

        public async Task<IEnumerable<CompetitionResult>> GetMenOpenResultsAsync(int year, string province)
        {
            return await GetCompetitionResultsAsync(new CompetitionDataRequest
            {
                CompetitionType = "All",
                Gender = "M",
                Province = province,
                Year = year,
                Unequipped = true
            });
        }

        public async Task<IEnumerable<CompetitionResult>> GetMenJuniorAndSubJuniorResultsAsync(int year, string province)
        {
            var juniorResults = await GetCompetitionResultsAsync(new CompetitionDataRequest
            {
                CompetitionType = "All",
                Gender = "M",
                Province = province,
                AgeCategory = "Junior",
                Year = year,
                Unequipped = true
            });

            var subJuniorResults = await GetCompetitionResultsAsync(new CompetitionDataRequest
            {
                CompetitionType = "All",
                Gender = "M",
                Province = province,
                AgeCategory = "Sub-Junior",
                Year = year,
                Unequipped = true
            });

            return MergeResults(juniorResults, subJuniorResults);
        }

        public async Task<IEnumerable<CompetitionResult>> GetMenMasterResultsAsync(int year, string province)
        {
            var dataRequest = new CompetitionDataRequest
            {
                CompetitionType = "All",
                Gender = "M",
                Province = province,
                Year = year,
                Unequipped = true,
                AgeCategory = "Master 1"
            };

            var master1Results = await GetCompetitionResultsAsync(dataRequest);

            dataRequest.AgeCategory = "Master 2";
            var master2Results = await GetCompetitionResultsAsync(dataRequest);
            
            dataRequest.AgeCategory = "Master 3";
            var master3Results = await GetCompetitionResultsAsync(dataRequest);

            dataRequest.AgeCategory = "Master 4";
            var master4Results = await GetCompetitionResultsAsync(dataRequest);

            return MergeResults(master1Results, master2Results, master3Results, master4Results);
        }

        public Task<IEnumerable<CompetitionResult>> GetMenBenchOnlyResultsAsync(int year, string province)
        {
            return GetCompetitionResultsAsync(new CompetitionDataRequest
            {
                CompetitionType = "Single",
                Gender = "M",
                Province = province,
                Year = year,
                Unequipped = true
            });
        }

        public Task<IEnumerable<CompetitionResult>> GetWomenOpenResultsAsync(int year, string province)
        {
            return GetCompetitionResultsAsync(new CompetitionDataRequest
            {
                CompetitionType = "All",
                Gender = "F",
                Province = province,
                Year = year,
                Unequipped = true
            });
        }

        public async Task<IEnumerable<CompetitionResult>> GetWomenJuniorAndSubJuniorResultsAsync(int year, string province)
        {
            var juniorResults = await GetCompetitionResultsAsync(new CompetitionDataRequest
            {
                CompetitionType = "All",
                Gender = "F",
                Province = province,
                AgeCategory = "Junior",
                Year = year,
                Unequipped = true
            });

            var subJuniorResults = await GetCompetitionResultsAsync(new CompetitionDataRequest
            {
                CompetitionType = "All",
                Gender = "F",
                Province = province,
                AgeCategory = "Sub-Junior",
                Year = year,
                Unequipped = true
            });

            return MergeResults(juniorResults, subJuniorResults);
        }

        public async Task<IEnumerable<CompetitionResult>> GetWomenMasterResultsAsync(int year, string province)
        {
            var dataRequest = new CompetitionDataRequest
            {
                CompetitionType = "All",
                Gender = "F",
                Province = province,
                Year = year,
                Unequipped = true,
                AgeCategory = "Master 1"
            };

            var master1Results = await GetCompetitionResultsAsync(dataRequest);

            dataRequest.AgeCategory = "Master 2";
            var master2Results = await GetCompetitionResultsAsync(dataRequest);

            dataRequest.AgeCategory = "Master 3";
            var master3Results = await GetCompetitionResultsAsync(dataRequest);

            dataRequest.AgeCategory = "Master 4";
            var master4Results = await GetCompetitionResultsAsync(dataRequest);

            return MergeResults(master1Results, master2Results, master3Results, master4Results);
        }

        public Task<IEnumerable<CompetitionResult>> GetWomenBenchOnlyResultsAsync(int year, string province)
        {
            return GetCompetitionResultsAsync(new CompetitionDataRequest
            {
                CompetitionType = "Single",
                Gender = "F",
                Province = province,
                Year = year,
                Unequipped = true
            });
        }

        public Task<IEnumerable<CompetitionResult>> GetOverallEquippedResultsAsync(int year, string province)
        {
            return GetCompetitionResultsAsync(new CompetitionDataRequest
            {
                CompetitionType = "All",
                Province = province,
                AgeCategory = "Open",
                Year = year,
                Unequipped = false
            });
        }

        #endregion

        private static HttpRequestMessage CreateHttpRequest(CompetitionDataRequest dataRequest)
        {
            return new HttpRequestMessage(HttpMethod.Post, CpuLifterDatabaseUrl)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
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
                    { "submit", "Search" } // Required to actually execute a search for some reason
                })
            };
        }

        private static IEnumerable<CompetitionResult> MergeResults(params IEnumerable<CompetitionResult>[] resultSets)
        {
            var mergedSets = Enumerable.Empty<CompetitionResult>();

            mergedSets = resultSets.Aggregate(mergedSets, (current, resultSet) => current.Concat(resultSet));

            return mergedSets.OrderByDescending(r => r.Points);
        }

        private static IEnumerable<CompetitionResult> SelectTopResultPerLifter(IEnumerable<CompetitionResult> results)
        {
            var bestResults = new List<CompetitionResult>();

            foreach (var result in results)
            {
                var existingResult =
                    bestResults.SingleOrDefault(cr => cr.AthleteName.ToLower().EditDistance(result.AthleteName.ToLower()) <= 2);

                if (existingResult == null)
                {
                    bestResults.Add(result);
                    continue;
                }

                if (existingResult.Points > result.Points)
                    continue;

                bestResults.Remove(existingResult);
                bestResults.Add(result);
            }

            return bestResults;
        }
    }
}
