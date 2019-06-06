using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = DatabaseLibrary.DatabaseModel.Type;

namespace DatabaseLibrary.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private KpiResearchersContext db;
        private AccountRepository accountRepository;
        private AccountPublicationRepository accountPublicationRepository;
        private ChairRepository chairRepository;
        private FacultyRepository facultyRepository;
        private OrcidAccountRepository orcidAccountRepository;        
        private OrcidPublicationRepository orcidPublicationRepository;
        private PublicationRepository publicationRepository;
        private ResearcherRepository researcherRepository;
        private ResearcherAccountRepository researcherAccountRepository;
        private ResearcherOrcidRepository researcherOrcidRepository;
        private TypeRepository typeRepository;

        public EFUnitOfWork()
        {
            db = KpiResearchersContext.GetKpiResearchersContext();
        }
        
        public IRepository<Account> Accounts
        {
            get
            {
                if (accountRepository == null)
                    accountRepository = new AccountRepository(db);
                return accountRepository;
            }
        }

        public IRepository<AccountPublication> AccountPublications
        {
            get
            {
                if (accountPublicationRepository == null)
                    accountPublicationRepository = new AccountPublicationRepository(db);
                return accountPublicationRepository;
            }
        }

        public IRepository<Chair> Chairs
        {
            get
            {
                if (chairRepository == null)
                    chairRepository = new ChairRepository(db);
                return chairRepository;
            }
        }

        public IRepository<Faculty> Faculties
        {
            get
            {
                if (facultyRepository == null)
                    facultyRepository = new FacultyRepository(db);
                return facultyRepository;
            }
        }

        public IRepository<OrcidAccount> OrcidAccounts
        {
            get
            {
                if (orcidAccountRepository == null)
                    orcidAccountRepository = new OrcidAccountRepository(db);
                return orcidAccountRepository;
            }
        }

        public IRepository<OrcidPublication> OrcidPublications
        {
            get
            {
                if (orcidPublicationRepository == null)
                    orcidPublicationRepository = new OrcidPublicationRepository(db);
                return orcidPublicationRepository;
            }
        }

        public IRepository<Publication> Publications
        {
            get
            {
                if (publicationRepository == null)
                    publicationRepository = new PublicationRepository(db);
                return publicationRepository;
            }
        }

        public IRepository<Researcher> Researchers
        {
            get
            {
                if (researcherRepository == null)
                    researcherRepository = new ResearcherRepository(db);
                return researcherRepository;
            }
        }

        public IRepository<ResearcherAccount> ResearcherAccounts
        {
            get
            {
                if (researcherAccountRepository == null)
                    researcherAccountRepository = new ResearcherAccountRepository(db);
                return researcherAccountRepository;
            }
        }

        public IRepository<ResearcherOrcid> ResearcherOrcids
        {
            get
            {
                if (researcherOrcidRepository == null)
                    researcherOrcidRepository = new ResearcherOrcidRepository(db);
                return researcherOrcidRepository;
            }
        }

        public IRepository<Type> Types
        {
            get
            {
                if (typeRepository == null)
                    typeRepository = new TypeRepository(db);
                return typeRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
