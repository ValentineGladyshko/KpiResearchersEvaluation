using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebLibrary.Entities;
using WebLibrary.Indexes;

namespace WebLibrary.Parsers
{
    public class Account
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public IList<MyPublication> results { get; set; }
        public bool ready { get; set; }
    }

    public class MyPublication
    {
        public string title { get; set; }
        public Journal journal { get; set; }
        public string ut { get; set; }
        public string datePublished { get; set; }
        public string urlAbsolute { get; set; }
        public string wosUrl { get; set; }
        public string wosCitationsUrl { get; set; }
        public string citationCount { get; set; }
    }

    public class Journal
    {
        public string name { get; set; }
        public string title { get; set; }
        public string urlAbsolute { get; set; }
        public string urlLogoInline { get; set; }
        public bool isInWosCoreCollection { get; set; }

    }

    public static class PublonsParser
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
                TypeId = 2
            };

            IWebDriver driver = WebSocket.PublonsDriver;
            lock (driver)
            {               
                driver.Url = @"https://publons.com/researcher/" + userId;

                string url = driver.Url;

                researcher.Id = url.Substring("https://publons.com/researcher/".Length).Split('/').First();

                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(driver.PageSource);
                var document = htmlDocument.DocumentNode;

                researcher.FullName = document.QuerySelector("#researcher-profile-page-content >" +
                    " div > div.researcher-profile-page-header > div > div.researcher-card-container.no-badges >" +
                    " div.researcher-card-header > h2").InnerText;

                if (!Int32.TryParse(document.QuerySelector("#researcher-profile-page-content > div > " +
                    "div.researcher-profile-page-header > div > div.researcher-card-container.no-badges > " +
                    "div.researcher-card-metrics.left-bar-figures > div:nth-child(2) > p").InnerText, out int citationCount))
                {
                    citationCount = -1;
                }
                researcher.CitationCount = citationCount;

                if (!Int32.TryParse(document.QuerySelector("#researcher-profile-page-content > div >" +
                    " div.researcher-profile-page-header > div > div.researcher-card-container.no-badges >" +
                    " div.researcher-card-metrics.left-bar-figures > div.left-bar-figure.left-bar-figure-large" +
                    " > p").InnerText.Split(' ').First(), out int hIndex))
                {
                    hIndex = -1;
                }
                researcher.HIndex = hIndex;              
                
                int page = 1;
                while (true)
                {             
                    var url1 = "https://publons.com/researcher/api/" + researcher.Id +
                        "/publications/?ordering=citations&page=" + page + "&per_page=30";                    

                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Account>(WebSocket.GetPageAsync(url1));

                    foreach (var item in result.results)
                    {
                        var publication = new Publication
                        {
                            Title = item.title,
                            Authors = string.Empty
                        };
                        if (item.journal != null)
                            publication.Source = item.journal.title;

                        citationCount = 0;
                        if (item.citationCount != null)
                            if (!Int32.TryParse(item.citationCount, out citationCount)) { }
                        publication.CitationCount = citationCount;

                        if (DateTime.TryParseExact(item.datePublished, "MMM yyyy",
                            System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                        {
                            publication.Date = date;
                        }
                        else if (DateTime.TryParseExact(item.datePublished, " yyyy",
                            System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                        {
                            publication.Date = date;
                        }

                        researcher.Publications.Add(publication);
                    }                    
                    if (result.count - page * 30 <= 0)
                        break;

                    page++;
                }
            }
            //driver.Quit();
            I10Index index = new I10Index();

            researcher.I10Index = index.GetIndex(researcher.Publications.Select(item => item.CitationCount));
            stopwatch.Stop();
            Console.WriteLine("publons: " + stopwatch.Elapsed.TotalSeconds + " seconds");
            return researcher;
        }
    }
}