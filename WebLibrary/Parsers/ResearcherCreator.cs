using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Interfaces;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebLibrary.Parsers
{
    public static class ResearcherSaver
    {
        public static void SaveResearcher(string faculty, string chair, string firstName,
            string middleName, string lastName, string userId, IUnitOfWork unitOfWork)
        {
            var localFaculty = new Faculty
            {
                FacultyName = faculty
            };
            var localResearcher = new Researcher();
            lock (unitOfWork)
            {
                var faculties = unitOfWork.Faculties.Find(item =>
                    item.FacultyName.ToLower() == localFaculty.FacultyName.ToLower());

                if (faculties.Count() == 0)
                {
                    unitOfWork.Faculties.Create(localFaculty);
                    unitOfWork.Save();
                }

                else
                {
                    localFaculty = faculties.First();
                }

                var localChair = new Chair
                {
                    ChairName = chair,
                    FacultyId = localFaculty.FacultyId
                };

                var chairs = unitOfWork.Chairs.Find(item =>
                    item.ChairName.ToLower() == localChair.ChairName.ToLower()
                    && item.FacultyId == localChair.FacultyId);

                if (chairs.Count() == 0)
                {
                    unitOfWork.Chairs.Create(localChair);
                    unitOfWork.Save();
                }

                else
                {
                    localChair = chairs.First();
                }

                localResearcher = new Researcher
                {
                    FirstName = firstName,
                    MiddleName = middleName,
                    LastName = lastName,
                    ChairId = localChair.ChairId
                };

                var researchers = unitOfWork.Researchers.Find(item =>
                    item.FirstName == localResearcher.FirstName
                    && item.MiddleName == localResearcher.MiddleName
                    && item.LastName == localResearcher.LastName
                    && item.ChairId == localResearcher.ChairId);

                if (researchers.Count() == 0)
                {
                    unitOfWork.Researchers.Create(localResearcher);
                    unitOfWork.Save();
                }

                else
                {
                    localResearcher = researchers.First();
                }
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var orcidAccount = OrcidParser.GetResearcherAsync(userId);
            stopwatch.Stop();
            Console.WriteLine("orcid: " + stopwatch.Elapsed.TotalSeconds + " seconds");
            lock (unitOfWork)
            {
                orcidAccount.Save(unitOfWork);

                var researcherOrcids = unitOfWork.ResearcherOrcids.Find(item =>
                    item.OrcidAccountId == orcidAccount.OrcidAccountId
                    && item.ResearcherId == localResearcher.ResearcherId);

                if (researcherOrcids.Count() == 0)
                {
                    unitOfWork.ResearcherOrcids.Create(new ResearcherOrcid
                    {
                        OrcidAccountId = orcidAccount.OrcidAccountId,
                        ResearcherId = localResearcher.ResearcherId
                    });
                    unitOfWork.Save();
                }                
            }
            string userSearch = orcidAccount.FullName;
            userSearch = userSearch.Replace(' ', '+').Replace(".", "");
            IWebDriver driver = WebSocket.Driver;
            IWebElement googleUserId;
            lock (driver)
            {
                driver.Url = "https://scholar.google.com.ua/citations?hl=uk&view_op=search_authors&mauthors="
            + userSearch + "+National+Technical+University+of+Ukraine";

                googleUserId = driver.FindElement(By.CssSelector("#gsc_sa_ccl > div > div > a"));
            }
            stopwatch.Restart();
            var googleAccount = Task<Entities.Researcher>.Run(() => GoogleSchoolarParser.GetResearcherAsync(googleUserId.GetAttribute("href").Replace(@"https://scholar.google.com.ua/citations?hl=uk&user=", "")));
            var publonsAccount = Task<Entities.Researcher>.Run(() => PublonsParser.GetResearcherAsync(orcidAccount.PublonsId));
            var scopusAccount = Task<Entities.Researcher>.Run(() => ScopusParser.GetResearcherAsync(orcidAccount.ScopusAuthorId));
            
            lock (unitOfWork)
            {
                stopwatch.Restart();
                int googleId = googleAccount.Result.Save(unitOfWork);
                int publonsId = publonsAccount.Result.Save(unitOfWork);                
                int scopusId = scopusAccount.Result.Save(unitOfWork);
                stopwatch.Stop();
                Console.WriteLine("db: " + stopwatch.Elapsed.TotalSeconds + " seconds");
                //driver.Quit();

                var researcherGoogles = unitOfWork.ResearcherAccounts.Find(item =>
                    item.AccountId == googleId
                    && item.ResearcherId == localResearcher.ResearcherId);

                if (researcherGoogles.Count() == 0)
                {
                    unitOfWork.ResearcherAccounts.Create(new ResearcherAccount
                    {
                        AccountId = googleId,
                        ResearcherId = localResearcher.ResearcherId
                    });
                    unitOfWork.Save();
                }

                var researcherPublons = unitOfWork.ResearcherAccounts.Find(item =>
                    item.AccountId == publonsId
                    && item.ResearcherId == localResearcher.ResearcherId);

                if (researcherPublons.Count() == 0)
                {
                    unitOfWork.ResearcherAccounts.Create(new ResearcherAccount
                    {
                        AccountId = publonsId,
                        ResearcherId = localResearcher.ResearcherId
                    });
                    unitOfWork.Save();
                }

                var researcherScopuses = unitOfWork.ResearcherAccounts.Find(item =>
                    item.AccountId == scopusId
                    && item.ResearcherId == localResearcher.ResearcherId);

                if (researcherScopuses.Count() == 0)
                {
                    unitOfWork.ResearcherAccounts.Create(new ResearcherAccount
                    {
                        AccountId = scopusId,
                        ResearcherId = localResearcher.ResearcherId
                    });
                    unitOfWork.Save();
                }

                unitOfWork.Save();
            }
        }
    }
}

