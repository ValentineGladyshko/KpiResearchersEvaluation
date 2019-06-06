using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatabaseLibrary.Repositories
{
    public class OrcidPublicationRepository : IRepository<OrcidPublication>
    {

        private KpiResearchersContext db;

        public OrcidPublicationRepository(KpiResearchersContext context)
        {
            this.db = context;
        }

        public IQueryable<OrcidPublication> GetAll()
        {
            return db.OrcidPublications.Include(item => item.OrcidAccount);
        }

        public OrcidPublication Get(int id)
        {
            return db.OrcidPublications.Find(id);
        }

        public IEnumerable<OrcidPublication> Find(Func<OrcidPublication, bool> predicate)
        {
            return db.OrcidPublications.Include(item => item.OrcidAccount).Where(predicate).ToList();
        }

        public OrcidPublication Create(OrcidPublication OrcidPublication)
        {
            return db.OrcidPublications.Add(OrcidPublication);
        }

        public void Update(OrcidPublication OrcidPublication)
        {
            db.Entry(OrcidPublication).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            OrcidPublication OrcidPublication = db.OrcidPublications.Find(id);
            if (OrcidPublication != null)
                db.OrcidPublications.Remove(OrcidPublication);
        }
    }
}


