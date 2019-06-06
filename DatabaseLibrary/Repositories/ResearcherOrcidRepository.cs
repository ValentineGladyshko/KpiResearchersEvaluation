using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatabaseLibrary.Repositories
{
    public class ResearcherOrcidRepository : IRepository<ResearcherOrcid>
    {

        private KpiResearchersContext db;

        public ResearcherOrcidRepository(KpiResearchersContext context)
        {
            this.db = context;
        }

        public IQueryable<ResearcherOrcid> GetAll()
        {
            return db.ResearcherOrcids.Include(item => item.OrcidAccount);
        }

        public ResearcherOrcid Get(int id)
        {
            return db.ResearcherOrcids.Find(id);
        }

        public IEnumerable<ResearcherOrcid> Find(Func<ResearcherOrcid, bool> predicate)
        {
            return db.ResearcherOrcids.Include(item => item.OrcidAccount).Where(predicate).ToList();
        }

        public ResearcherOrcid Create(ResearcherOrcid ResearcherOrcid)
        {
            return db.ResearcherOrcids.Add(ResearcherOrcid);
        }

        public void Update(ResearcherOrcid ResearcherOrcid)
        {
            db.Entry(ResearcherOrcid).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ResearcherOrcid ResearcherOrcid = db.ResearcherOrcids.Find(id);
            if (ResearcherOrcid != null)
                db.ResearcherOrcids.Remove(ResearcherOrcid);
        }
    }
}


