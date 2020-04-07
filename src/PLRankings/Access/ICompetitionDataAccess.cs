using System.Collections.Generic;
using System.Threading.Tasks;
using PLRankings.Models;

namespace PLRankings.Access
{
    public interface ICompetitionDataAccess
    {
        Task<IEnumerable<CompetitionResult>> GetMenOpenResultsAsync(int year, string province);
        
        Task<IEnumerable<CompetitionResult>> GetMenJuniorAndSubJuniorResultsAsync(int year, string province);

        Task<IEnumerable<CompetitionResult>> GetMenMasterResultsAsync(int year, string province);

        Task<IEnumerable<CompetitionResult>> GetMenBenchOnlyResultsAsync(int year, string province);

        Task<IEnumerable<CompetitionResult>> GetWomenOpenResultsAsync(int year, string province);

        Task<IEnumerable<CompetitionResult>> GetWomenJuniorAndSubJuniorResultsAsync(int year, string province);

        Task<IEnumerable<CompetitionResult>> GetWomenMasterResultsAsync(int year, string province);

        Task<IEnumerable<CompetitionResult>> GetWomenBenchOnlyResultsAsync(int year, string province);

        Task<IEnumerable<CompetitionResult>> GetOverallEquippedResultsAsync(int year, string province);

        Task<IEnumerable<CompetitionResult>> GetInternationalResultsAsync(int year, string province);
    }
}
