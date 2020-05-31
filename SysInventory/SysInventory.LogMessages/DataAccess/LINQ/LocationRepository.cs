using System;
using System.Linq;
using System.Linq.Expressions;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.DataAccess.LINQ
{
    internal class LocationRepository : LinqBaseRepository<ILocation>
    {
        public override void Add(ILocation entity)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext())
            {
                entity.Id = Guid.NewGuid();
                Context.GetTable<Location>().InsertOnSubmit((Location)entity);
                Context.SubmitChanges();
            }
        }
        public override long Count()
        {
            using (Context = new SysInventoryLinqSqlContextDataContext(ConnectionString))
                return Context.GetTable<Location>().LongCount();
        }
        public override long Count(Expression<Func<ILocation, bool>> whereExpression)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext(ConnectionString))
                return whereExpression != null
                    ? Context.GetTable<Location>().LongCount(whereExpression)
                    : Context.GetTable<Location>().LongCount();
        }
        public override void Delete(ILocation entity)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext())
            {
                Context.GetTable<Location>().DeleteOnSubmit(Context.GetTable<Location>().First(x => x.Id == entity.Id));
                Context.SubmitChanges();
            }
        }
        public override IQueryable<ILocation> GetAll()
        {
            Context = new SysInventoryLinqSqlContextDataContext(ConnectionString);
            return Context.GetTable<Location>().OrderBy(x => x.Name);
        }
        public override IQueryable<ILocation> GetAll(Expression<Func<ILocation, bool>> whereExpression)
        {
            Context = new SysInventoryLinqSqlContextDataContext(ConnectionString);
            var query = whereExpression!=null ? Context.GetTable<Location>().Where(whereExpression) : Context.GetTable<Location>();
            return query.OrderBy(x => x.Name);
        }
        public override ILocation GetSingle<TKey>(TKey pkValue)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext(ConnectionString))
            {
                var id = Guid.Parse(pkValue.ToString());
                return Context.GetTable<Location>().First(x => x.Id == id);
            }
        }
        public override void Update(ILocation entity)
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
