using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatabaseLibrary.Repositories
{
    public class ResearcherRepository : IRepository<Researcher>
    {

        private KpiResearchersContext db;

        public ResearcherRepository(KpiResearchersContext context)
        {
            this.db = context;
        }

        public IQueryable<Researcher> GetAll()
        {
            return db.Researchers.Include(item => item.ResearcherAccounts).Include(item => item.ResearcherOrcids);
        }

        public Researcher Get(int id)
        {
            return db.Researchers.Find(id);
        }

        public IEnumerable<Researcher> Find(Func<Researcher, bool> predicate)
        {
            return db.Researchers.Include(item => item.ResearcherAccounts).Include(item => item.ResearcherOrcids).Where(predicate).ToList();
        }

        public Researcher Create(Researcher Researcher)
        {
            return db.Researchers.Add(Researcher);
        }

        public void Update(Researcher Researcher)
        {
            db.Entry(Researcher).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Researcher Researcher = db.Researchers.Find(id);
            if (Researcher != null)
                db.Researchers.Remove(Researcher);
        }
    }
}


