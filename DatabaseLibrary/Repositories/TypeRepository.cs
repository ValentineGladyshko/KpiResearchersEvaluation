using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Type = DatabaseLibrary.DatabaseModel.Type;

namespace DatabaseLibrary.Repositories
{
    public class TypeRepository : IRepository<Type>
    {

        private KpiResearchersContext db;

        public TypeRepository(KpiResearchersContext context)
        {
            this.db = context;
        }

        public IQueryable<Type> GetAll()
        {
            return db.Types;
        }

        public Type Get(int id)
        {
            return db.Types.Find(id);
        }

        public IEnumerable<Type> Find(Func<Type, bool> predicate)
        {
            return db.Types.Where(predicate).ToList();
        }

        public Type Create(Type Type)
        {
            return db.Types.Add(Type);
        }

        public void Update(Type Type)
        {
            db.Entry(Type).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Type Type = db.Types.Find(id);
            if (Type != null)
                db.Types.Remove(Type);
        }
    }
}


