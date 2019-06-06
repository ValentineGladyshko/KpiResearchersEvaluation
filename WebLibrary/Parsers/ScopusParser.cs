using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebLibrary.Entities;
using WebLibrary.Indexes;

namespace WebLibrary.Parsers
{
    public static class ScopusParser
    {
        public static Researcher GetResearcherAsync(string userId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("message", nameof(userId));
            }

            var researcher = new Researcher
            {
                Id = userId,
                TypeId = 1
            };

            IWebDriver driver = WebSocket.ScopusDriver;
            lock (driver)
            {
                driver.Url = "https://www.scopus.com/authid/detail.uri?authorId=" + userId;

                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(driver.PageSource);
                var document = htmlDocument.DocumentNode;

                researcher.FullName = document.QuerySelector("#authDetailsNameSection > div >" +
                    " div:nth-child(1) > div.nameSection.pull-left.col-md-5.noPadding > h2").InnerText;
                if (!Int32.TryParse(document.QuerySelector("#totalCiteCount").InnerText, out int citationCount))
                {
                    citationCount = -1;
                }
                researcher.CitationCount = citationCount;

                if (!Int32.TryParse(document.QuerySelector("#authorDetailsHindex > div > " +
                    "div.panel-body > span").InnerText, out int hIndex))
                {
                    hIndex = -1;
                }
                researcher.HIndex = hIndex;

                driver.Url = "https://www.scopus.com/author/document/retrieval.uri?authorId=" + userId +
                    "&tabSelected=docLi&sortType=cp-f&resultCount=200";
                
                htmlDocument.LoadHtml(driver.PageSource);
                document = htmlDocument.DocumentNode;

                var publicationsList = document.QuerySelectorAll("tr.searchArea");
                int i = 0;
                foreach (var item in publicationsList)
                {
                    i++;
                    try
                    {
                        var publication = new Publication
                        {
                            Title = item.QuerySelector("td:nth-child(2)").InnerText,
                            Authors = item.QuerySelector("td:nth-child(3)").InnerText,
                            Source = item.QuerySelector("td:nth-child(5)").InnerText
                        };

                        if (Int32.TryParse(item.QuerySelector("td:nth-child(6)").InnerText, out citationCount))
                        {
                            publication.CitationCount = citationCount;
                        }
                        else
                        {
                            publication.CitationCount = -1;
                        }

                        if (DateTime.TryParseExact(item.QuerySelector("td:nth-child(4)").InnerText,
                            "yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                        {
                            publication.Date = date;
                        }

                        researcher.Publications.Add(publication);
                    }
                    catch
                    {
                        Console.WriteLine(i);
                    }
                }
            }
            //driver.Quit();
            I10Index index = new I10Index();

            researcher.I10Index = index.GetIndex(researcher.Publications.Select(item => item.CitationCount));
            stopwatch.Stop();
            Console.WriteLine("scopus: " + stopwatch.Elapsed.TotalSeconds + " seconds");
            return researcher;
        }
    }
}
