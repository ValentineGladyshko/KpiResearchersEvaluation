using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatabaseLibrary.Repositories
{
    public class PublicationRepository : IRepository<Publication>
    {

        private KpiResearchersContext db;

        public PublicationRepository(KpiResearchersContext context)
        {
            this.db = context;
        }

        public IQueryable<Publication> GetAll()
        {
            return db.Publications;
        }

        public Publication Get(int id)
        {
            return db.Publications.Find(id);
        }

        public IEnumerable<Publication> Find(Func<Publication, bool> predicate)
        {
            return db.Publications.Where(predicate).ToList();
        }

        public Publication Create(Publication Publication)
        {
            return db.Publications.Add(Publication);
        }

        public void Update(Publication Publication)
        {
            db.Entry(Publication).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Publication Publication = db.Publications.Find(id);
            if (Publication != null)
                db.Publications.Remove(Publication);
        }
    }
}


