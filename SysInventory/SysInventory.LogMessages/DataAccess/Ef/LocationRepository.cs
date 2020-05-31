using System;
using System.Linq;
using System.Linq.Expressions;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.DataAccess.Ef
{
    internal class LocationRepository : EfBaseRepository<ILocation>
    {
        public override void Add(ILocation entity)
        {
            using (Context = new SysInventoryEntities())
            {
                Context.LocationEfs.Add((LocationEf)entity);
                Context.SaveChanges();
            }
        }
        public override long Count()
        {
            using (Context = new SysInventoryEntities())
            {
                return Context.LocationEfs.LongCount();
            }
        }
        public override long Count(Expression<Func<ILocation, bool>> whereExpression)
        {
            using (Context = new SysInventoryEntities())
            {
                return whereExpression != null ? Context.LocationEfs.LongCount(whereExpression) : Context.LocationEfs.LongCount();
            }
        }
        public override void Delete(ILocation entity)
        {
            using (Context = new SysInventoryEntities())
            {
                var found = Context.LocationEfs.Find(entity.Id);
                if(found==null) return;
                Context.LocationEfs.Remove(found);
                Context.SaveChanges();
            }
        }
        public override IQueryable<ILocation> GetAll()
        {
            Context = new SysInventoryEntities();
            return Context.LocationEfs.OrderBy(x => x.Name);
        }
        public override IQueryable<ILocation> GetAll(Expression<Func<ILocation, bool>> whereExpression)
        {
            Context = new SysInventoryEntities();
            var query = whereExpression != null ? Context.LocationEfs.Where(whereExpression) : Context.LocationEfs;
            return query.OrderBy(x => x.Name);
        }
        public override ILocation GetSingle<TKey>(TKey pkValue)
        {
            using (Context = new SysInventoryEntities())
                return Context.LocationEfs.Find(pkValue);
        }
        public override void Update(ILocation entity)
        {
            using (Context = new SysInventoryEntities())
            {
                var found = Context.LocationEfs.Find(entity.Id);
                if(found==null) return;
                found.ParentId = entity.ParentId;
                found.PoDId = entity.PoDId;
                found.Name = entity.Name;
                Context.SaveChanges();
            }
        }
    }
}
