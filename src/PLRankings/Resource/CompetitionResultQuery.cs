namespace PLRankings.Resource
{
    public class CompetitionResultQuery
    {
        public string CompetitionType { get; set; }

        public string Gender { get; set; }

        public string Province { get; set; }

        public string AgeCategory { get; set; }

        public string WeightClass { get; set; }

        public int? Year { get; set; }

        public string AthleteName { get; set; }

        public string ContestName { get; set; }

        public bool? Unequipped { get; set; } 
    }
}
