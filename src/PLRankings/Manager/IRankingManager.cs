using System.Collections.Generic;
using System.Threading.Tasks;

namespace PLRankings.Manager
{
    public interface IRankingManager
    {
        public Task<IEnumerable<(string title, string content)>> ExportRankingsToCsvAsync(int year);
    }
}
