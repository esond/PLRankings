using System;

namespace PLRankings.Models
{
    public class Result
    {
        public string Contest { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public string ContestType { get; set; }

        public string Gender { get; set; }

        public string LifterName { get; set; }

        public string Province { get; set; }

        public double Bodyweight { get; set; }

        public int Class { get; set; }

        public AgeCategory AgeCategory { get; set; }

        public double Squat { get; set; }

        public double Bench { get; set; }

        public double Deadlift { get; set; }

        public double Total { get; set; }

        public double WilksPoints { get; set; }

        public string Year { get; set; }

        public bool Unequipped { get; set; }
    }

    public enum AgeCategory
    {
        SubJunior,
        Junior,
        Open,
        M1,
        M2,
        M3
    }
}
