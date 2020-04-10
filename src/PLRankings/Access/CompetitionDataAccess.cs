using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PLRankings.Models;
using PLRankings.Resource;

namespace PLRankings.Access
{
    public class CompetitionDataAccess : ICompetitionDataAccess
    {
        private readonly ICompetitionDatabase _database;

        public CompetitionDataAccess(ICompetitionDatabase database)
        {
            _database = database;
        }

        #region Implementation of ICompetitionDataAccess

        public async Task<IEnumerable<CompetitionResult>> GetMenOpenResultsAsync(int year, string province)
        {
            var results = SelectTopResultPerLifter(await _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = CompetitionType.ThreeLift,
                Sex = Sex.Male,
                Province = province,
                Year = year,
                Equipment = Equipment.Raw
            }));

            return results;
        }

        public async Task<IEnumerable<CompetitionResult>> GetMenJuniorAndSubJuniorResultsAsync(int year, string province)
        {
            var juniorResults = await _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = CompetitionType.ThreeLift,
                Sex = Sex.Male,
                Province = province,
                AgeCategory = "Junior",
                Year = year,
                Equipment = Equipment.Raw
            });

            var subJuniorResults = await _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = CompetitionType.ThreeLift,
                Sex = Sex.Male,
                Province = province,
                AgeCategory = "Sub-Junior",
                Year = year,
                Equipment = Equipment.Raw
            });

            return SelectTopResultPerLifter(MergeResults(juniorResults, subJuniorResults));
        }

        public async Task<IEnumerable<CompetitionResult>> GetMenMasterResultsAsync(int year, string province)
        {
            var dataRequest = new CompetitionResultQuery
            {
                CompetitionType = CompetitionType.ThreeLift,
                Sex = Sex.Male,
                Province = province,
                Year = year,
                Equipment = Equipment.Raw,
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
                CompetitionType = CompetitionType.BenchOnly,
                Sex = Sex.Male,
                Province = province,
                Year = year,
                Equipment = Equipment.Raw
            }));
        }

        public async Task<IEnumerable<CompetitionResult>> GetWomenOpenResultsAsync(int year, string province)
        {
            return SelectTopResultPerLifter(await _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = CompetitionType.ThreeLift,
                Sex = Sex.Female,
                Province = province,
                Year = year,
                Equipment = Equipment.Raw
            }));
        }

        public async Task<IEnumerable<CompetitionResult>> GetWomenJuniorAndSubJuniorResultsAsync(int year, string province)
        {
            var juniorResults = await _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = CompetitionType.ThreeLift,
                Sex = Sex.Female,
                Province = province,
                AgeCategory = "Junior",
                Year = year,
                Equipment = Equipment.Raw
            });

            var subJuniorResults = await _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = CompetitionType.ThreeLift,
                Sex = Sex.Female,
                Province = province,
                AgeCategory = "Sub-Junior",
                Year = year,
                Equipment = Equipment.Raw
            });

            return SelectTopResultPerLifter(MergeResults(juniorResults, subJuniorResults));
        }

        public async Task<IEnumerable<CompetitionResult>> GetWomenMasterResultsAsync(int year, string province)
        {
            var dataRequest = new CompetitionResultQuery
            {
                CompetitionType = CompetitionType.ThreeLift,
                Sex = Sex.Female,
                Province = province,
                Year = year,
                Equipment = Equipment.Raw,
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
                CompetitionType = CompetitionType.BenchOnly,
                Sex = Sex.Female,
                Province = province,
                Year = year,
                Equipment = Equipment.Raw
            });
        }

        public async Task<IEnumerable<CompetitionResult>> GetOverallEquippedResultsAsync(int year, string province)
        {
            return SelectTopResultPerLifter(await _database.QueryAsync(new CompetitionResultQuery
            {
                CompetitionType = CompetitionType.ThreeLift,
                Province = province,
                AgeCategory = "Open",
                Year = year,
                Equipment = Equipment.SinglePly
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
                    bestResults.FirstOrDefault(cr => cr.AthleteName.ToLower().EditDistance(result.AthleteName.ToLower()) <= 2);

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
