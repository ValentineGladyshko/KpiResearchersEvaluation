using DatabaseLibrary.DatabaseModel;
using System;
using System.Xml;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WebLibrary.Parsers
{
    public static class OrcidParser
    {
        public static OrcidAccount GetResearcherAsync(string userId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("message", nameof(userId));
            }

            var orcidAccount = new OrcidAccount();

            var url = "https://pub.orcid.org/v2.1/" + userId + "/record";
            var xmlString = WebSocket.GetPageAsync(url);

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xmlString);
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xDoc.NameTable);
            namespaceManager.AddNamespace("record", "http://www.orcid.org/ns/record");
            namespaceManager.AddNamespace("person", "http://www.orcid.org/ns/person");
            namespaceManager.AddNamespace("personal-details", "http://www.orcid.org/ns/personal-details");
            namespaceManager.AddNamespace("external-identifier", "http://www.orcid.org/ns/external-identifier");
            namespaceManager.AddNamespace("common", "http://www.orcid.org/ns/common");
            namespaceManager.AddNamespace("activities", "http://www.orcid.org/ns/activities");
            namespaceManager.AddNamespace("work", "http://www.orcid.org/ns/work");            
            XmlElement xRoot = xDoc.DocumentElement;

            orcidAccount.OrcId = userId;
            
            orcidAccount.FullName = xRoot.SelectSingleNode("/record:record/person:person/person:name/" +
                "personal-details:given-names", namespaceManager).InnerText + " " + xRoot.SelectSingleNode("/record:record/person:person" +
                "/person:name/personal-details:family-name", namespaceManager).InnerText;
            var identifiers = xRoot.SelectNodes("/record:record/person:person/external-identifier:external-identifiers/" +
                "external-identifier:external-identifier", namespaceManager);

            foreach (XmlNode item in identifiers)
            {                
                var type = item.SelectSingleNode("common:external-id-type", namespaceManager).InnerText;
                var id = item.SelectSingleNode("common:external-id-value", namespaceManager).InnerText;
                var accountUrl = item.SelectSingleNode("common:external-id-url", namespaceManager).InnerText;
                if (type == "Scopus Author ID")
                {
                    orcidAccount.ScopusAuthorId = id;
                    orcidAccount.ScopusLink = accountUrl;
                }
                else if (type == "ResearcherID")
                {
                    orcidAccount.PublonsId = id;
                    orcidAccount.PublonsLink = accountUrl;
                }
            }

            var publications = xRoot.SelectNodes("/record:record/activities:activities-summary" +
                "/activities:works/activities:group/work:work-summary", namespaceManager);

            foreach (XmlNode item in publications)
            {
                var publication = new OrcidPublication();
                
                publication.Title = item.SelectSingleNode("work:title/common:title", namespaceManager).InnerText;
                var yearNode = item.SelectSingleNode("common:publication-date/common:year", namespaceManager);
                var monthNode = item.SelectSingleNode("common:publication-date/common:month", namespaceManager);
                var dayNode = item.SelectSingleNode("common:publication-date/common:day", namespaceManager);

                int year = 1;
                int month = 1;
                int day = 1;

                if (yearNode != null)
                {
                    if (!Int32.TryParse(yearNode.InnerText, out year))
                    {
                        year = 1;
                    }
                }
                if (monthNode != null)
                {
                    if (!Int32.TryParse(monthNode.InnerText, out month))
                    {
                        month = 1;
                    }
                }
                if (dayNode != null)
                {
                    if (!Int32.TryParse(dayNode.InnerText, out day))
                    {
                        day = 1;
                    }
                }
                DateTime date = new DateTime(year, month, day);

                publication.Date = date;

                orcidAccount.OrcidPublications.Add(publication);
            }
            orcidAccount.PublicationsCount = orcidAccount.OrcidPublications.Count;
            stopwatch.Stop();
            Console.WriteLine("orcid: " + stopwatch.Elapsed.TotalSeconds + " seconds");
            return orcidAccount;
        }        
    }
}
