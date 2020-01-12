using System.Collections.Generic;
using System.Threading.Tasks;
using PLRankings.Data.Contracts;
using PLRankings.Models;

namespace PLRankings.Data
{
    public interface ICompetitionDataAccess
    {
        Task<IEnumerable<CompetitionResult>> GetCompetitionResultsAsync(CompetitionDataRequest dataRequest);
    }
}
