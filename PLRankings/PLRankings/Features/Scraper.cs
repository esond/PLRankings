﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using PLRankings.Models;

namespace PLRankings.Features
{
    public class Scraper
    {
        private readonly string _outputFolder;
        private readonly DateTime _provincials2015Date = new DateTime(2015, 7, 4);
        private readonly DateTime _provincials2016Date = new DateTime(2016, 6, 11);
        private readonly IEnumerable<Uri> _resultsUris;

        public Scraper(string outputFolder, params string[] resultsUris)
        {
            _outputFolder = outputFolder;
            _resultsUris = resultsUris.Select(r => new Uri(r)).ToList();
        }

        public void GetData()
        {
            string resultsString = string.Empty;

            foreach (Uri resultsUri in _resultsUris)
            {
                string raw = GetRawHtml(resultsUri);

                int indexOfFirstResult = raw.IndexOf("<TR bgColor=\"WHITE\">", StringComparison.Ordinal);
                int indexOfResultsEnd = raw.IndexOf("</TABLE>", StringComparison.Ordinal);

                resultsString += raw.Substring(indexOfFirstResult, indexOfResultsEnd - indexOfFirstResult);
            }

            string[] rows = Regex.Split(resultsString, "\r\n|\r|\n");

            List<CompetitionResult> results = new List<CompetitionResult>();

            foreach (
                CompetitionResult result in
                rows.Where(r => !string.IsNullOrEmpty(r))
                    .Select(BuildResult)
                    .Where(cr => (cr.Date >= _provincials2015Date) && (cr.Date < _provincials2016Date))
                    .OrderByDescending(cr => cr.WilksPoints))
            {
                if (!results.Select(cr => cr.LifterName).Contains(result.LifterName))
                    results.Add(result);
            }

            IEnumerable<CompetitionResult> orderedResults = results.OrderByDescending(cr => cr.WilksPoints);

            StringBuilder builder = new StringBuilder();

            foreach (var line in ToCsv(orderedResults))
            {
                builder.AppendLine(line);
            }

            File.WriteAllText(_outputFolder + "document.csv", builder.ToString());
        }

        private string GetRawHtml(Uri resultsUri)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(resultsUri);
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (
                    StreamReader reader = new StreamReader(response.GetResponseStream(),
                        Encoding.GetEncoding(response.CharacterSet)))
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

            try
            {
                result.Date = DateTime.ParseExact(tableCells[1], "dd-MMM-yy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
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

        public static IEnumerable<string> ToCsv<T>(IEnumerable<T> objectlist, string separator = ",", bool header = true)
        {
            FieldInfo[] fields = typeof(T).GetFields();
            PropertyInfo[] properties = typeof(T).GetProperties();
            if (header)
            {
                yield return String.Join(separator, fields.Select(f => f.Name).Concat(properties.Select(p => p.Name)).ToArray());
            }
            foreach (var o in objectlist)
            {
                yield return string.Join(separator, fields.Select(f => (f.GetValue(o) ?? "").ToString())
                    .Concat(properties.Select(p => (p.GetValue(o, null) ?? "").ToString())).ToArray());
            }
        }
    }
}