using DatabaseLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.Indexes;

namespace WebLibrary.Entities
{
    public class FullResearcher
    {

        public int ResearcherId { get; set; }

        public string FullName { get; set; }

        public int GoogleHIndex { get; set; }

        public int GoogleHIndex5 { get; set; }

        public int PublonsHIndex { get; set; }

        public int PublonsHIndex5 { get; set; }

        public int ScopusHIndex { get; set; }

        public int ScopusHIndex5 { get; set; }

        public int GooglePublicationsCount { get; set; }

        public int PublonsPublicationsCount { get; set; }

        public int ScopusPublicationsCount { get; set; }

        public FullResearcher(DatabaseLibrary.DatabaseModel.Researcher researcher, IUnitOfWork unitOfWork)
        {
            HIndex hIndex = new HIndex();
            I10Index i10Index = new I10Index();
            ResearcherId = researcher.ResearcherId;
            FullName = researcher.LastName + " " + researcher.FirstName + " " + researcher.MiddleName;
            GoogleHIndex = hIndex.GetIndex(unitOfWork.Publications.Find(item => item.AccountPublications
                .Any(publication => publication.Account.TypeId == 3 && publication.Account.ResearcherAccounts
                .Any(item1 => item1.ResearcherId == ResearcherId))).Select(item => item.CitationCount));

            GoogleHIndex5 = i10Index.GetIndex(unitOfWork.Publications.Find(item => item.AccountPublications
                .Any(publication => publication.Account.TypeId == 3 && publication.Account.ResearcherAccounts
                .Any(item1 => item1.ResearcherId == ResearcherId))).Select(item => item.CitationCount));

            PublonsHIndex = hIndex.GetIndex(unitOfWork.Publications.Find(item => item.AccountPublications
                .Any(publication => publication.Account.TypeId == 2 && publication.Account.ResearcherAccounts
                .Any(item1 => item1.ResearcherId == ResearcherId))).Select(item => item.CitationCount));

            PublonsHIndex5 = i10Index.GetIndex(unitOfWork.Publications.Find(item => item.AccountPublications
                .Any(publication => publication.Account.TypeId == 2 && publication.Account.ResearcherAccounts
                .Any(item1 => item1.ResearcherId == ResearcherId))).Select(item => item.CitationCount));

            ScopusHIndex = hIndex.GetIndex(unitOfWork.Publications.Find(item => item.AccountPublications
                .Any(publication => publication.Account.TypeId == 1 && publication.Account.ResearcherAccounts
                .Any(item1 => item1.ResearcherId == ResearcherId))).Select(item => item.CitationCount));

            ScopusHIndex5 = i10Index.GetIndex(unitOfWork.Publications.Find(item => item.AccountPublications
                .Any(publication => publication.Account.TypeId == 1 && publication.Account.ResearcherAccounts
                .Any(item1 => item1.ResearcherId == ResearcherId))).Select(item => item.CitationCount));
        }
    }
}


