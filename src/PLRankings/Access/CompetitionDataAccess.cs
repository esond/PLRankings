﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PLRankings.Models;
using PLRankings.Resource;

namespace PLRankings.Access
{
    public class HtmlCompetitionDataAccess : ICompetitionDataAccess
    {
        private readonly ICompetitionDatabase _database;

        public HtmlCompetitionDataAccess(ICompetitionDatabase database)
        {
            _database = database;
        }

        #region Implementation of ICompetitionDataAccess

        private IEnumerable<CompetitionResult> SanitizeResults(IEnumerable<CompetitionResult> results)
        {
            foreach (var result in results)
            {
                if (result.ContestType != "All" && result.ContestType != "Single")
                    continue;

                yield return result;
            }
        }

        public async Task<IEnumerable<CompetitionResult>> GetMenOpenResultsAsync(int year, string province)
        {
            return SelectTopResultPerLifter(await _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = "All",
                Gender = "M",
                Province = province,
                Year = year,
                Unequipped = true
            }));
        }

        public async Task<IEnumerable<CompetitionResult>> GetMenJuniorAndSubJuniorResultsAsync(int year, string province)
        {
            var juniorResults = await _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = "All",
                Gender = "M",
                Province = province,
                AgeCategory = "Junior",
                Year = year,
                Unequipped = true
            });

            var subJuniorResults = await _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = "All",
                Gender = "M",
                Province = province,
                AgeCategory = "Sub-Junior",
                Year = year,
                Unequipped = true
            });

            return SelectTopResultPerLifter(MergeResults(juniorResults, subJuniorResults));
        }

        public async Task<IEnumerable<CompetitionResult>> GetMenMasterResultsAsync(int year, string province)
        {
            var dataRequest = new CompetitionResultQuery
            {
                CompetitionType = "All",
                Gender = "M",
                Province = province,
                Year = year,
                Unequipped = true,
                AgeCategory = "Master 1"
            };

            var master1Results = await _database.QueryAsync(dataRequest);

            dataRequest.AgeCategory = "Master 2";
            var master2Results = await _database.QueryAsync(dataRequest);
            
            dataRequest.AgeCategory = "Master 3";
            var master3Results = await _database.QueryAsync(dataRequest);

            dataRequest.AgeCategory = "Master 4";
            var master4Results = await _database.QueryAsync(dataRequest);

            return SelectTopResultPerLifter(MergeResults(master1Results, master2Results, master3Results, master4Results));
        }

        public async Task<IEnumerable<CompetitionResult>> GetMenBenchOnlyResultsAsync(int year, string province)
        {
            return SelectTopResultPerLifter(await _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = "Single",
                Gender = "M",
                Province = province,
                Year = year,
                Unequipped = true
            }));
        }

        public async Task<IEnumerable<CompetitionResult>> GetWomenOpenResultsAsync(int year, string province)
        {
            return SelectTopResultPerLifter(await _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = "All",
                Gender = "F",
                Province = province,
                Year = year,
                Unequipped = true
            }));
        }

        public async Task<IEnumerable<CompetitionResult>> GetWomenJuniorAndSubJuniorResultsAsync(int year, string province)
        {
            var juniorResults = await _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = "All",
                Gender = "F",
                Province = province,
                AgeCategory = "Junior",
                Year = year,
                Unequipped = true
            });

            var subJuniorResults = await _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = "All",
                Gender = "F",
                Province = province,
                AgeCategory = "Sub-Junior",
                Year = year,
                Unequipped = true
            });

            return SelectTopResultPerLifter(MergeResults(juniorResults, subJuniorResults));
        }

        public async Task<IEnumerable<CompetitionResult>> GetWomenMasterResultsAsync(int year, string province)
        {
            var dataRequest = new CompetitionResultQuery
            {
                CompetitionType = "All",
                Gender = "F",
                Province = province,
                Year = year,
                Unequipped = true,
                AgeCategory = "Master 1"
            };

            var master1Results = await _database.QueryAsync(dataRequest);

            dataRequest.AgeCategory = "Master 2";
            var master2Results = await _database.QueryAsync(dataRequest);

            dataRequest.AgeCategory = "Master 3";
            var master3Results = await _database.QueryAsync(dataRequest);

            dataRequest.AgeCategory = "Master 4";
            var master4Results = await _database.QueryAsync(dataRequest);

            return SelectTopResultPerLifter(MergeResults(master1Results, master2Results, master3Results, master4Results));
        }

        public Task<IEnumerable<CompetitionResult>> GetWomenBenchOnlyResultsAsync(int year, string province)
        {
            return _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = "Single",
                Gender = "F",
                Province = province,
                Year = year,
                Unequipped = true
            });
        }

        public async Task<IEnumerable<CompetitionResult>> GetOverallEquippedResultsAsync(int year, string province)
        {
            return SelectTopResultPerLifter(await _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = "All",
                Province = province,
                AgeCategory = "Open",
                Year = year,
                Unequipped = false
            }));
        }

        public async Task<IEnumerable<CompetitionResult>> GetInternationalResultsAsync(int year, string province)
        {
            var allInternationalResults = await _database.QueryAsync(new CompetitionResultQuery
            {
                Year = year,
                Province = "CAN"
            });

            foreach (var internationalResult in SelectTopResultPerLifter(allInternationalResults))
            {
                await Task.Delay(500);

                var individualCompetitionResults = await _database.QueryAsync(new CompetitionResultQuery
                {
                    AthleteName = internationalResult.AthleteName
                });

                var mostRecentProvincialResult = individualCompetitionResults
                    .Where(r => r.Province != "CAN")
                    .OrderByDescending(r => r.Date)
                    .FirstOrDefault();

                if (mostRecentProvincialResult == null)
                {
                    Console.WriteLine($"{internationalResult.AthleteName} has no provincial results!");
                    continue;
                }

                Console.WriteLine($"{internationalResult.AthleteName} is a {mostRecentProvincialResult.Province} lifter.");
            }

            return allInternationalResults;
        }

        #endregion

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