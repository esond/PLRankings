using System;
using System.Collections.Generic;
using PLRankings.Models;

namespace PLRankings
{
    /// <summary>
    ///     Adapted from the OpenPowerlifting IPF points calculator.
    ///     <see cref="http://www.ipfpointscalculator.com/" />
    ///     <seealso cref="http://gitlab.com/openpowerlifting/ipf-points-calculator/-/blob/master/index.html" />
    /// </summary>
    public static class PointsCalculator
    {
        private static Dictionary<Sex, Dictionary<Equipment, Dictionary<CompetitionType, double[]>>> Constants =
            new Dictionary<Sex, Dictionary<Equipment, Dictionary<CompetitionType, double[]>>>
            {
                {
                    Sex.Male, new Dictionary<Equipment, Dictionary<CompetitionType, double[]>>
                    {
                        {
                            Equipment.Raw, new Dictionary<CompetitionType, double[]>
                            {
                                {
                                    CompetitionType.ThreeLift, new[]
                                    {
                                        310.67, 857.785, 53.216, 147.0835
                                    }
                                },
                                {
                                    CompetitionType.BenchOnly, new[]
                                    {
                                        86.4745, 259.155, 17.57845, 53.122
                                    }
                                }
                            }
                        },
                        {
                            Equipment.SinglePly, new Dictionary<CompetitionType, double[]>
                            {
                                {
                                    CompetitionType.ThreeLift, new[]
                                    {
                                        387.265, 1121.28, 80.6324, 222.4896
                                    }
                                },
                                {
                                    CompetitionType.BenchOnly, new[]
                                    {
                                        133.94, 441.465, 35.3938, 113.0057
                                    }
                                }
                            }
                        }
                    }
                },
                {
                    Sex.Female, new Dictionary<Equipment, Dictionary<CompetitionType, double[]>>
                    {
                        {
                            Equipment.Raw, new Dictionary<CompetitionType, double[]>
                            {
                                {
                                    CompetitionType.ThreeLift, new[]
                                    {
                                        310.67, 857.785, 53.216, 147.0835
                                    }
                                },
                                {
                                    CompetitionType.BenchOnly, new[]
                                    {
                                        86.4745, 259.155, 17.57845, 53.122
                                    }
                                }
                            }
                        },
                        {
                            Equipment.SinglePly, new Dictionary<CompetitionType, double[]>
                            {
                                {
                                    CompetitionType.ThreeLift, new[]
                                    {
                                        387.265, 1121.28, 80.6324, 222.4896
                                    }
                                },
                                {
                                    CompetitionType.BenchOnly, new[]
                                    {
                                        133.94, 441.465, 35.3938, 113.0057
                                    }
                                }
                            }
                        }
                    }
                }
            };

        public static double CalculateWilksPoints(Sex sex, double bodyweight, double total)
        {
            return sex switch
            {
                Sex.Male => (total * GetMaleWilksCoefficient(bodyweight)),
                Sex.Female => (total * GetFemaleWilksCoefficient(bodyweight)),
                _ => throw new ArgumentOutOfRangeException(nameof(sex), sex, null)
            };
        }

        public static double CalculateIpfPoints(Sex sex, Equipment equipment, CompetitionType competitionType,
            double bodyweight, double total)
        {
            var parameters = Constants[sex][equipment][competitionType];

            var mean = parameters[0] * Math.Log(bodyweight) - parameters[1];
            var deviation = parameters[2] * Math.Log(bodyweight) - parameters[3];

            var points = 500 + 100 * (total - mean) / deviation;

            if (double.IsNaN(points) || points < 0 || bodyweight < 40)
                points = 0;

            return points;
        }

        private static double GetMaleWilksCoefficient(double bw)
        {
            var effectiveBw = Math.Min(Math.Max(bw, 40.0), 201.9);
            return WilksPoly(-216.0475144, 16.2606339, -0.002388645, -0.00113732, 7.01863E-06, -1.291E-08, effectiveBw);
        }

        private static double GetFemaleWilksCoefficient(double bw)
        {
            var effectiveBw = Math.Min(Math.Max(bw, 26.51), 154.53);
            return WilksPoly(594.31747775582, -27.23842536447, 0.82112226871, -0.00930733913, 0.00004731582,
                -0.00000009054, effectiveBw);
        }

        private static double WilksPoly(double a, double b, double c, double d, double e, double f, double x)
        {
            double x2 = x * x,
                x3 = x2 * x,
                x4 = x3 * x,
                x5 = x4 * x;

            return 500.0 / (a + b * x + c * x2 + d * x3 + e * x4 + f * x5);
        }
    }
}