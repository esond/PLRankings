using System;

namespace PLRankings.Models
{
    public class CompetitionResult
    {
        public string ContestName { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public CompetitionType CompetitionType { get; set; }

        public Sex Sex { get; set; }

        public string AthleteName { get; set; }

        public string Province { get; set; }

        public double Bodyweight { get; set; }

        public string WeightClass { get; set; }

        public string AgeCategory { get; set; }

        public double Squat { get; set; }

        public double Bench { get; set; }

        public double Deadlift { get; set; }

        public double Total { get; set; }

        public double Points { get; set; }

        public Equipment Equipment { get; set; }
    }
}
