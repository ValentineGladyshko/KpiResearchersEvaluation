using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
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
    public static class GoogleSchoolarParser
    {
        public static Researcher GetResearcherAsync(string userId)
        {            
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("message", nameof(userId));
            }

            var researcher = new Researcher
            {
                Id = userId,
                TypeId = 3
            };


            var url = "https://scholar.google.com.ua/citations?hl=uk&user=" + userId + "&cstart=0&pagesize=100";

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(WebSocket.GetPageAsync(url));
            var document = htmlDocument.DocumentNode;


            researcher.FullName = document.QuerySelector("#gsc_prf_in").InnerText;
            if (!Int32.TryParse(document.QuerySelector("table#gsc_rsb_st tbody " +
                "tr:nth-child(1) td:nth-child(2)").InnerText, out int citationCount))
            {
                citationCount = -1;
            }
            researcher.CitationCount = citationCount;

            if (!Int32.TryParse(document.QuerySelector("table#gsc_rsb_st tbody" +
                " tr:nth-child(2) td:nth-child(2)").InnerText, out int hIndex))
            {
                hIndex = -1;
            }
            researcher.HIndex = hIndex;

            if (!Int32.TryParse(document.QuerySelector("table#gsc_rsb_st tbody" +
                " tr:nth-child(3) td:nth-child(2)").InnerText, out int i10Index))
            {
                i10Index = -1;
            }
            researcher.I10Index = i10Index;

            var publicationsList = document.QuerySelectorAll("tr.gsc_a_tr");

            foreach (var item in publicationsList)
            {
                var publication = new Publication
                {
                    Title = item.QuerySelector("a.gsc_a_at").InnerText,
                    Authors = item.QuerySelector(":nth-child(2)").InnerText,
                    Source = item.QuerySelector(":nth-child(3)").InnerText
                };

                if (!Int32.TryParse(item.QuerySelector("a.gsc_a_ac.gs_ibl").InnerText, out citationCount))
                {
                    citationCount = -1;
                }
                publication.CitationCount = citationCount;

                if (DateTime.TryParseExact(item.QuerySelector("span.gsc_a_h.gsc_a_hc.gs_ibl").InnerText,
                    "yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    publication.Date = date;
                }

                researcher.Publications.Add(publication);
            }            

            return researcher;
        }
    }
}

