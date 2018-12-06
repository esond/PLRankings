using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using PLRankings.Models;

namespace PLRankings.Features
{
    public static class Scraper
    {
        public static IEnumerable<CompetitionResult> GetResults(IEnumerable<string> resultsUris)
        {
            IEnumerable<string> rows = GetRows(resultsUris).Where(r => !string.IsNullOrEmpty(r)).ToList();

            var seasonResults = rows.Select(BuildResult);

            var topResults = new List<CompetitionResult>();

            foreach (var result in seasonResults.OrderByDescending(cr => cr.WilksPoints))
            {
                var existingResult =
                    topResults.SingleOrDefault(cr => EditDistance(cr.LifterName, result.LifterName) < 2);

                if (existingResult == null)
                {
                    topResults.Add(result);
                    continue;
                }

                if (existingResult.WilksPoints < result.WilksPoints)
                {
                    topResults.Remove(existingResult);
                    topResults.Add(result);
                }
            }

            return topResults;
        }

        private static string[] GetRows(IEnumerable<string> uris)
        {
            var resultsString = string.Empty;

            foreach (var resultsUri in uris.Select(x => new Uri(x)))
            {
                var raw = GetRawHtml(resultsUri);

                var indexOfFirstResult = raw.IndexOf("<TR bgColor=\"WHITE\">", StringComparison.Ordinal);
                var indexOfResultsEnd = raw.IndexOf("</TABLE>", StringComparison.Ordinal);

                if (indexOfFirstResult == -1)
                    continue;

                resultsString += raw.Substring(indexOfFirstResult, indexOfResultsEnd - indexOfFirstResult);
            }

            return Regex.Split(resultsString, "\r\n|\r|\n");
        }

        private static string GetRawHtml(Uri resultsUri)
        {
            var request = (HttpWebRequest) WebRequest.Create(resultsUri);
            var response = (HttpWebResponse) request.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
                throw new HttpException(response.StatusCode.ToString());

            using (var reader = new StreamReader(response.GetResponseStream(),
                Encoding.GetEncoding(response.CharacterSet)))
            {
                return reader.ReadToEnd();
            }
        }

        private static CompetitionResult BuildResult(string tableRow)
        {
            // 7 is the length of the <FONT> tags.
            var substringStart = tableRow.IndexOf("<FONT >", StringComparison.Ordinal) + 7;
            var substringLength = tableRow.LastIndexOf("</TD>", StringComparison.Ordinal) - substringStart - 7;

            var tableData = tableRow.Substring(substringStart, substringLength);

            var tableCells = tableData.Split(new[] {"</FONT></TD><TD><FONT >"},
                StringSplitOptions.RemoveEmptyEntries).Select(tc => tc.Trim()).ToArray();

            var result = new CompetitionResult();

            result.ContestName = tableCells[0];

            try
            {
                result.Date = DateTime.ParseExact(tableCells[1], "dd-MMM-yy", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                result.Date = DateTime.ParseExact(tableCells[1], "d-MMM-yy", CultureInfo.InvariantCulture);
            }
            
            result.Location = tableCells[2].Replace(",", string.Empty);
            result.ContestType = tableCells[3];
            result.Gender = tableCells[4];
            result.LifterName = tableCells[5];
            result.Province = tableCells[6];
            double.TryParse(tableCells[7], out var bw);
            result.Bodyweight = bw;
            // unused column for retired bodyweight classes
            result.Class = tableCells[9];
            result.AgeCategory = tableCells[10];

            double.TryParse(tableCells[11], out var squat);
            result.Squat = squat;

            result.Bench = double.Parse(tableCells[12]);

            double.TryParse(tableCells[13], out var deadlift);
            result.Deadlift = deadlift;

            result.Total = double.Parse(tableCells[14]);
            result.WilksPoints = double.Parse(tableCells[15]);
            result.Year = tableCells[16];

            if (tableCells[17] == "yes")
                result.Unequipped = true;

            return result;
        }

        /// <summary>Computes the Levenshtein Edit Distance between two enumerables.</summary>
        /// <typeparam name="T">The type of the items in the enumerables.</typeparam>
        /// <param name="x">The first enumerable.</param>
        /// <param name="y">The second enumerable.</param>
        /// <returns>The edit distance.</returns>
        public static int EditDistance<T>(IEnumerable<T> x, IEnumerable<T> y)
            where T : IEquatable<T>
        {
            if (x == null)
                throw new ArgumentNullException(nameof(x));

            if (y == null)
                throw new ArgumentNullException(nameof(y));

            var first = x as IList<T> ?? new List<T>(x);
            var second = y as IList<T> ?? new List<T>(y);

            int n = first.Count, m = second.Count;

            if (n == 0)
                return m;

            if (m == 0)
                return n;

            int curRow = 0, nextRow = 1;

            int[][] rows = {new int[m + 1], new int[m + 1]};

            for (var j = 0; j <= m; ++j)
                rows[curRow][j] = j;

            for (var i = 1; i <= n; ++i)
            {
                rows[nextRow][0] = i;

                for (var j = 1; j <= m; ++j)

                {
                    var dist1 = rows[curRow][j] + 1;
                    var dist2 = rows[nextRow][j - 1] + 1;
                    var dist3 = rows[curRow][j - 1] + (first[i - 1].Equals(second[j - 1]) ? 0 : 1);

                    rows[nextRow][j] = Math.Min(dist1, Math.Min(dist2, dist3));
                }

                if (curRow == 0)
                {
                    curRow = 1;
                    nextRow = 0;
                }
                else
                {
                    curRow = 0;
                    nextRow = 1;
                }
            }

            return rows[curRow][m];
        }
    }
}