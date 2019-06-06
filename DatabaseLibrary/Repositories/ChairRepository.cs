using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatabaseLibrary.Repositories
{
    public class ChairRepository : IRepository<Chair>
    {

        private KpiResearchersContext db;

        public ChairRepository(KpiResearchersContext context)
        {
            this.db = context;
        }

        public IQueryable<Chair> GetAll()
        {
            return db.Chairs.Include(item => item.Researchers);
        }

        public Chair Get(int id)
        {
            return db.Chairs.Find(id);
        }

        public IEnumerable<Chair> Find(Func<Chair, bool> predicate)
        {
            return db.Chairs.Include(item => item.Researchers).Where(predicate).ToList();
        }

        public Chair Create(Chair Chair)
        {
            return db.Chairs.Add(Chair);
        }

        public void Update(Chair Chair)
        {
            db.Entry(Chair).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Chair Chair = db.Chairs.Find(id);
            if (Chair != null)
                db.Chairs.Remove(Chair);
        }
    }
}


