using System;
using System.Linq;
using System.Linq.Expressions;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.DataAccess.LINQ
{
    internal class LocationRepository : LinqBaseRepository<Location>
    {
        public override void Add(Location entity)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext())
            {
                entity.Id = Guid.NewGuid();
                Context.GetTable<Location>().InsertOnSubmit(entity);
                Context.SubmitChanges();
            }
        }
        public override long Count(Expression<Func<Location, bool>> whereExpression)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext(ConnectionString))
                return Context.GetTable<Location>().LongCount(x => x.Name != string.Empty);
        }
        public override void Delete(Location entity)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext())
            {
                Context.GetTable<Location>().Attach(entity);
                Context.GetTable<Location>().DeleteOnSubmit(entity);
                Context.SubmitChanges();
            }
        }
        public override IQueryable<Location> GetAll(Expression<Func<Location, bool>> whereExpression)
        {
            Context = new SysInventoryLinqSqlContextDataContext(ConnectionString);
            return Context.GetTable<Location>().Where(x => x.ParentId == null);
        }
        public override Location GetSingle<TKey>(TKey pkValue)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext(ConnectionString))
            {
                var id = Guid.Parse(pkValue.ToString());
                return Context.GetTable<Location>().First(x => x.Id == id);
            }
        }
        public override void Update(Location entity)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext())
            {
                var found = Context.GetTable<Location>().First(e => e.Id == entity.Id);
                found.ParentId = entity.ParentId;
                found.PoDId = entity.PoDId;
                found.Name = entity.Name;
                Context.SubmitChanges();
            }
        }
    }
}
