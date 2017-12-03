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
                var existingResult = topResults.SingleOrDefault(
                    cr => string.Equals(cr.LifterName, result.LifterName, StringComparison.OrdinalIgnoreCase));

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

            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (var reader = new StreamReader(response.GetResponseStream(),
                    Encoding.GetEncoding(response.CharacterSet)))
                {
                    return reader.ReadToEnd();
                }
            }

            throw new HttpException();
        }

        private static CompetitionResult BuildResult(string tableRow)
        {
            // 7 is the length of the <FONT> tags.
            var substringStart = tableRow.IndexOf("<FONT >", StringComparison.Ordinal) + 7;
            var substringLength = tableRow.LastIndexOf("</TD>", StringComparison.Ordinal) - substringStart - 7;

            var tableData = tableRow.Substring(substringStart, substringLength);

            var tableCells = tableData.Split(new[] {"</FONT></TD><TD><FONT >"},
                StringSplitOptions.RemoveEmptyEntries);

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
            result.Bodyweight = double.Parse(tableCells[7]);
            // unused column for retired bodyweight classes
            result.Class = tableCells[9];
            result.AgeCategory = tableCells[10];

            double squat;
            double.TryParse(tableCells[11], out squat);
            result.Squat = squat;

            result.Bench = double.Parse(tableCells[12]);

            double deadlift;
            double.TryParse(tableCells[13], out deadlift);
            result.Deadlift = deadlift;

            result.Total = double.Parse(tableCells[14]);
            result.WilksPoints = double.Parse(tableCells[15]);
            result.Year = tableCells[16];

            if (tableCells[17] == "yes")
                result.Unequipped = true;

            return result;
        }
    }
}