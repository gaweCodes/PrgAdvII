using System;
using System.Linq;
using System.Linq.Expressions;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.DataAccess.Ef
{
    internal class LogRepository : EfBaseRepository<ILogEntry>
    {
        public override void Add(ILogEntry entity)
        {
            using (Context = new SysInventoryEntities())
            {
                Context.LogMessageAdd(entity.PoD, entity.Hostname, entity.Severity, entity.Message);
            }
        }
        public override long Count()
        {
            using (Context = new SysInventoryEntities())
            {
                return Context.v_logentries.LongCount();
            }
        }
        public override long Count(Expression<Func<ILogEntry, bool>> whereExpression)
        {
            using (Context = new SysInventoryEntities())
            {
                return whereExpression != null ? Context.v_logentries.LongCount(whereExpression) : Context.v_logentries.LongCount();
            }
        }
        public override void Delete(ILogEntry entity)
        {
            using (Context = new SysInventoryEntities())
            {
                Context.LogEfs.Remove(Context.LogEfs.Find(entity.Id));
                Context.SaveChanges();
            }
        }
        public override IQueryable<ILogEntry> GetAll()
        {
            Context = new SysInventoryEntities();
            return Context.v_logentries;
        }
        public override IQueryable<ILogEntry> GetAll(Expression<Func<ILogEntry, bool>> whereExpression)
        {
            Context = new SysInventoryEntities();
            return whereExpression != null ? Context.v_logentries.Where(whereExpression) : Context.v_logentries;
        }
        public override ILogEntry GetSingle<TKey>(TKey pkValue)
        {
            using (Context = new SysInventoryEntities())
                return Context.v_logentries.Find(pkValue);
        }
        public override void Update(ILogEntry entity)
        {
            using (Context = new SysInventoryEntities())
                Context.LogClear(entity.Id);
        }
    }
}
