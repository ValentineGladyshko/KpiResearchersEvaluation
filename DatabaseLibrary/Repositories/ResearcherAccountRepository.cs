using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatabaseLibrary.Repositories
{
    public class ResearcherAccountRepository : IRepository<ResearcherAccount>
    {

        private KpiResearchersContext db;

        public ResearcherAccountRepository(KpiResearchersContext context)
        {
            this.db = context;
        }

        public IQueryable<ResearcherAccount> GetAll()
        {
            return db.ResearcherAccounts.Include(item => item.Account);
        }

        public ResearcherAccount Get(int id)
        {
            return db.ResearcherAccounts.Find(id);
        }

        public IEnumerable<ResearcherAccount> Find(Func<ResearcherAccount, bool> predicate)
        {
            return db.ResearcherAccounts.Include(item => item.Account).Where(predicate).ToList();
        }

        public ResearcherAccount Create(ResearcherAccount ResearcherAccount)
        {
            return db.ResearcherAccounts.Add(ResearcherAccount);
        }

        public void Update(ResearcherAccount ResearcherAccount)
        {
            db.Entry(ResearcherAccount).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ResearcherAccount ResearcherAccount = db.ResearcherAccounts.Find(id);
            if (ResearcherAccount != null)
                db.ResearcherAccounts.Remove(ResearcherAccount);
        }
    }
}


