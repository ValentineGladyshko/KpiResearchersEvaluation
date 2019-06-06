using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatabaseLibrary.Repositories
{
    public class FacultyRepository : IRepository<Faculty>
    {

        private KpiResearchersContext db;

        public FacultyRepository(KpiResearchersContext context)
        {
            this.db = context;
        }

        public IQueryable<Faculty> GetAll()
        {
            return db.Faculties.Include(item => item.Chairs);
        }

        public Faculty Get(int id)
        {
            return db.Faculties.Find(id);
        }

        public IEnumerable<Faculty> Find(Func<Faculty, bool> predicate)
        {
            return db.Faculties.Include(item => item.Chairs).Where(predicate).ToList();
        }

        public Faculty Create(Faculty Faculty)
        {
            return db.Faculties.Add(Faculty);
        }

        public void Update(Faculty Faculty)
        {
            db.Entry(Faculty).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Faculty Faculty = db.Faculties.Find(id);
            if (Faculty != null)
                db.Faculties.Remove(Faculty);
        }
    }
}


