using WebLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using WebLibrary.Parsers;
using WebLibrary.Entities;
using DatabaseLibrary.Interfaces;
using DatabaseLibrary.Repositories;
using System.IO;
using WebLibrary.Indexes;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = WebSocket.Driver;
            IUnitOfWork unitOfWork = new EFUnitOfWork();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ResearcherSaver.SaveResearcher("fiot3", "asoiu4", "Yuri", "M", "Gordienko", "0000-0002-7700-5649", unitOfWork);
            stopwatch.Stop();
            Console.WriteLine("all: " + stopwatch.Elapsed.TotalSeconds + " seconds");
            //var pub = unitOfWork.Publications.Find(item => item.AccountPublications.Any(ui => ui.Account.ResearcherAccounts.Any(y => y.Researcher.Chair.Faculty.FacultyName == "fiot")));
            ////Stopwatch stopwatch = new Stopwatch();
            //Console.WriteLine(DateIndex.GetIndex(new I10Index(), pub, DateTime.MinValue, DateTime.MaxValue));
            stopwatch.Restart();
            Researcher researchery = GoogleSchoolarParser.GetResearcherAsync("5Ih2GqMAAAAJ");
            //researchery.Save(unitOfWork);
            stopwatch.Stop();
            //Console.WriteLine("google: " + stopwatch.Elapsed.TotalSeconds + " seconds");

            stopwatch.Restart();
            Researcher researcherq = PublonsParser.GetResearcherAsync("2379217");
            //researcherq.Save(unitOfWork);
            stopwatch.Stop();
            //Console.WriteLine(stopwatch.Elapsed.TotalSeconds + " seconds");

            stopwatch.Restart();
            Researcher researcherw = ScopusParser.GetResearcherAsync("7101756666");
            //researcherw.Save(unitOfWork);
            stopwatch.Stop();
            //Console.WriteLine(stopwatch.Elapsed.TotalSeconds + " seconds");

            stopwatch.Restart();
            researchery = GoogleSchoolarParser.GetResearcherAsync("5Ih2GqMAAAAJ");
            //researchery.Save(unitOfWork);
            stopwatch.Stop();
            //Console.WriteLine(stopwatch.Elapsed.TotalSeconds + " seconds");

            stopwatch.Restart();
            researcherq = PublonsParser.GetResearcherAsync("2379217");
            //researcherq.Save(unitOfWork);
            stopwatch.Stop();
            //Console.WriteLine(stopwatch.Elapsed.TotalSeconds + " seconds");

            stopwatch.Restart();
            researcherw = ScopusParser.GetResearcherAsync("7101756666");
            //researcherw.Save(unitOfWork);
            stopwatch.Stop();
            //Console.WriteLine(stopwatch.Elapsed.TotalSeconds + " seconds");

            stopwatch.Restart();
            researchery = GoogleSchoolarParser.GetResearcherAsync("5Ih2GqMAAAAJ");
            //researchery.Save(unitOfWork);
            stopwatch.Stop();
            //Console.WriteLine(stopwatch.Elapsed.TotalSeconds + " seconds");

            stopwatch.Restart();
            researcherq = PublonsParser.GetResearcherAsync("2379217");
            //researcherq.Save(unitOfWork);
            stopwatch.Stop();
            //Console.WriteLine(stopwatch.Elapsed.TotalSeconds + " seconds");

            stopwatch.Restart();
            researcherw = ScopusParser.GetResearcherAsync("7101756666");
            //researcherw.Save(unitOfWork);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.TotalSeconds + " seconds");

            stopwatch.Restart();
            researchery = GoogleSchoolarParser.GetResearcherAsync("5Ih2GqMAAAAJ");
            //researchery.Save(unitOfWork);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.TotalSeconds + " seconds");

            stopwatch.Restart();
            researcherq = PublonsParser.GetResearcherAsync("2379217");
            //researcherq.Save(unitOfWork);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.TotalSeconds + " seconds");

            stopwatch.Restart();
            researcherw = ScopusParser.GetResearcherAsync("7101756666");
            //researcherw.Save(unitOfWork);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.TotalSeconds + " seconds");

            Console.WriteLine("====================");
            Console.ReadKey();
        }
    }
}
