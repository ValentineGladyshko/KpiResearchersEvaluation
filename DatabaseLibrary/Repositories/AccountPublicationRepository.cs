using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatabaseLibrary.Repositories
{
    public class AccountPublicationRepository : IRepository<AccountPublication>
    {

        private KpiResearchersContext db;

        public AccountPublicationRepository(KpiResearchersContext context)
        {
            this.db = context;
        }

        public IQueryable<AccountPublication> GetAll()
        {
            return db.AccountPublications.Include(item => item.Publication);
        }

        public AccountPublication Get(int id)
        {
            return db.AccountPublications.Find(id);
        }

        public IEnumerable<AccountPublication> Find(Func<AccountPublication, bool> predicate)
        {
            return db.AccountPublications.Include(item => item.Publication).Where(predicate).ToList();
        }

        public AccountPublication Create(AccountPublication AccountPublication)
        {
            return db.AccountPublications.Add(AccountPublication);
        }

        public void Update(AccountPublication AccountPublication)
        {
            db.Entry(AccountPublication).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            AccountPublication AccountPublication = db.AccountPublications.Find(id);
            if (AccountPublication != null)
                db.AccountPublications.Remove(AccountPublication);
        }
    }
}


