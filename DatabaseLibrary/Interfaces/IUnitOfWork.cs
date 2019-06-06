using DatabaseLibrary.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = DatabaseLibrary.DatabaseModel.Type;

namespace DatabaseLibrary.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Account> Accounts { get; }
        IRepository<AccountPublication> AccountPublications { get; }
        IRepository<Chair> Chairs { get; }
        IRepository<Faculty> Faculties { get; }
        IRepository<OrcidAccount> OrcidAccounts { get; }        
        IRepository<OrcidPublication> OrcidPublications { get; }
        IRepository<Publication> Publications { get; }
        IRepository<Researcher> Researchers { get; }
        IRepository<ResearcherAccount> ResearcherAccounts { get; }
        IRepository<ResearcherOrcid> ResearcherOrcids { get; }
        IRepository<Type> Types { get; }        
        void Save();
    }
}
