using System;
using System.IO;
using System.Net;
using System.Text;

namespace PLRankings.Features
{
    public class Scraper
    {
        private readonly string _outputFile;
        private readonly Uri _resultsUri;

        public Scraper(string resultsUri, string outputFile)
        {
            _outputFile = outputFile;
            _resultsUri = new Uri(resultsUri);
        }

        public void GetData()
        {
            string result = GetRawHtml();

            File.WriteAllText(_outputFile, result);
        }

        private string GetRawHtml()
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(_resultsUri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet)))
                {
                    return reader.ReadToEnd();
                }
            }

            return "Error";
        }
    }
}
