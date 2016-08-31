using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using PLRankings.Models;

namespace PLRankings.Features
{
    public class Scraper
    {
        private readonly string _outputFolder;
        private readonly Uri _resultsUri;

        public Scraper(string resultsUri, string outputFolder)
        {
            _outputFolder = outputFolder;
            _resultsUri = new Uri(resultsUri);
        }

        public void GetData()
        {
            string raw = GetRawHtml();

            int indexOfFirstResult = raw.IndexOf("<TR bgColor=\"WHITE\">", StringComparison.Ordinal);
            int indexOfResultsEnd = raw.IndexOf("</TABLE>", StringComparison.Ordinal);

            string resultsString = raw.Substring(indexOfFirstResult, indexOfResultsEnd - indexOfFirstResult);

            string [] rows = Regex.Split(resultsString, "\r\n|\r|\n");

            List<CompetitionResult> results = new List<CompetitionResult>();

            foreach (CompetitionResult result in rows.Where(r => !string.IsNullOrEmpty(r)).Select(BuildResult))
            {
                if (!results.Select(cr => cr.LifterName).Contains(result.LifterName))
                {
                    results.Add(result);
                }
            }

            StringBuilder builder = new StringBuilder();

            for (int i = 1; i <= results.Count; i++)
            {
                builder.AppendLine($"{i}. {results[i - 1]}");
            }

            File.WriteAllText(_outputFolder + "document.txt", builder.ToString());
        }

        private string GetRawHtml()
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(_resultsUri);
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet)))
                {
                    return reader.ReadToEnd();
                }
            }

            throw new HttpException();
        }

        private CompetitionResult BuildResult(string tableRow)
        {
            // 7 is the length of the <FONT> tags.
            int substringStart = tableRow.IndexOf("<FONT >", StringComparison.Ordinal) + 7;
            int substringLength = tableRow.LastIndexOf("</TD>", StringComparison.Ordinal) - substringStart - 7;

            string tableData = tableRow.Substring(substringStart, substringLength);

            string[] tableCells = tableData.Split(new[] {"</FONT></TD><TD><FONT >"},
                StringSplitOptions.RemoveEmptyEntries);

            CompetitionResult result = new CompetitionResult();

            result.ContestName = tableCells[0];
            result.Date = tableCells[1];
            result.Location = tableCells[2];
            result.ContestType = tableCells[3];
            result.Gender = tableCells[4];
            result.LifterName = tableCells[5];
            result.Province = tableCells[6];
            result.Bodyweight = double.Parse(tableCells[7]);
            // Column for retired bodyweight classes
            result.Class = tableCells[9];
            result.AgeCategory = tableCells[10];
            result.Squat = double.Parse(tableCells[11]);
            result.Bench = double.Parse(tableCells[12]);
            result.Deadlift = double.Parse(tableCells[13]);
            result.Total = double.Parse(tableCells[14]);
            result.WilksPoints = double.Parse(tableCells[15]);
            result.Year = tableCells[16];

            if (tableCells[17] == "yes")
                result.Unequipped = true;

            return result;
        }
    }
}
