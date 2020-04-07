using System.Collections.Generic;
using System.Threading.Tasks;
using PLRankings.Models;

namespace PLRankings.Resource
{
    public interface ICompetitionDatabase
    {
        Task<IEnumerable<CompetitionResult>> QueryAsync(CompetitionResultQuery query);
    }
}
