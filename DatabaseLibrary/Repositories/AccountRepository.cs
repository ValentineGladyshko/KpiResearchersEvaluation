using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatabaseLibrary.Repositories
{
    public class AccountRepository : IRepository<Account>
    {

        private KpiResearchersContext db;

        public AccountRepository(KpiResearchersContext context)
        {
            this.db = context;
        }

        public IQueryable<Account> GetAll()
        {
            return db.Accounts.Include(item => item.Type);
        }

        public Account Get(int id)
        {
            return db.Accounts.Find(id);
        }

        public IEnumerable<Account> Find(Func<Account, bool> predicate)
        {
            return db.Accounts.Include(item => item.Type).Where(predicate).ToList();
        }

        public Account Create(Account Account)
        {
            return db.Accounts.Add(Account);
        }

        public void Update(Account Account)
        {
            db.Entry(Account).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Account Account = db.Accounts.Find(id);
            if (Account != null)
                db.Accounts.Remove(Account);
        }
    }
}


