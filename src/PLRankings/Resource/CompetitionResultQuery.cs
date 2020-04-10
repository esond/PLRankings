using PLRankings.Models;

namespace PLRankings.Resource
{
    public class CompetitionResultQuery
    {
        public CompetitionType CompetitionType { get; set; }

        public Sex Sex { get; set; }

        public string Province { get; set; }

        public string AgeCategory { get; set; }

        public string WeightClass { get; set; }

        public int? Year { get; set; }

        public string AthleteName { get; set; }

        public string ContestName { get; set; }

        public Equipment? Equipment { get; set; } 
    }
}
