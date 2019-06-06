using DatabaseLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.Indexes;

namespace WebLibrary.Entities
{
    public class Chair
    {  

        public int ChairId { get; set; }
        
        public string ChairName { get; set; }

        public int GoogleHIndex { get; set; }

        public int GoogleHIndex5 { get; set; }

        public int PublonsHIndex { get; set; }

        public int PublonsHIndex5 { get; set; }

        public int ScopusHIndex { get; set; }

        public int ScopusHIndex5 { get; set; }


        public Chair(DatabaseLibrary.DatabaseModel.Chair chair, IUnitOfWork unitOfWork, IIndex index)
        {
            I10Index i10Index = new I10Index();
            ChairId = chair.ChairId;
            ChairName = chair.ChairName;
            GoogleHIndex = index.GetIndex(unitOfWork.Publications.Find(item => item.AccountPublications
                .Any(publication => publication.Account.TypeId == 3 && publication.Account.ResearcherAccounts
                .Any(researcher => researcher.Researcher.ChairId == ChairId))).Select(item => item.CitationCount));

            GoogleHIndex5 = i10Index.GetIndex(unitOfWork.Publications.Find(item => item.AccountPublications
                 .Any(publication => publication.Account.TypeId == 3 && publication.Account.ResearcherAccounts
                 .Any(researcher => researcher.Researcher.ChairId == ChairId))).Select(item => item.CitationCount));

            PublonsHIndex = index.GetIndex(unitOfWork.Publications.Find(item => item.AccountPublications
                .Any(publication => publication.Account.TypeId == 2 && publication.Account.ResearcherAccounts
                .Any(researcher => researcher.Researcher.ChairId == ChairId))).Select(item => item.CitationCount));

            PublonsHIndex5 = i10Index.GetIndex(unitOfWork.Publications.Find(item => item.AccountPublications
                .Any(publication => publication.Account.TypeId == 2 && publication.Account.ResearcherAccounts
                .Any(researcher => researcher.Researcher.ChairId == ChairId))).Select(item => item.CitationCount));

            ScopusHIndex = index.GetIndex(unitOfWork.Publications.Find(item => item.AccountPublications
                .Any(publication => publication.Account.TypeId == 1 && publication.Account.ResearcherAccounts
                .Any(researcher => researcher.Researcher.ChairId == ChairId))).Select(item => item.CitationCount));

            ScopusHIndex5 = i10Index.GetIndex(unitOfWork.Publications.Find(item => item.AccountPublications
                .Any(publication => publication.Account.TypeId == 1 && publication.Account.ResearcherAccounts
                .Any(researcher => researcher.Researcher.ChairId == ChairId))).Select(item => item.CitationCount));
        }
    }
}
