using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatabaseLibrary.Repositories
{
    public class OrcidAccountRepository : IRepository<OrcidAccount>
    {

        private KpiResearchersContext db;

        public OrcidAccountRepository(KpiResearchersContext context)
        {
            this.db = context;
        }

        public IQueryable<OrcidAccount> GetAll()
        {
            return db.OrcidAccounts;
        }

        public OrcidAccount Get(int id)
        {
            return db.OrcidAccounts.Find(id);
        }

        public IEnumerable<OrcidAccount> Find(Func<OrcidAccount, bool> predicate)
        {
            return db.OrcidAccounts.Where(predicate).ToList();
        }

        public OrcidAccount Create(OrcidAccount OrcidAccount)
        {
            return db.OrcidAccounts.Add(OrcidAccount);
        }

        public void Update(OrcidAccount OrcidAccount)
        {
            db.Entry(OrcidAccount).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            OrcidAccount OrcidAccount = db.OrcidAccounts.Find(id);
            if (OrcidAccount != null)
                db.OrcidAccounts.Remove(OrcidAccount);
        }
    }
}


