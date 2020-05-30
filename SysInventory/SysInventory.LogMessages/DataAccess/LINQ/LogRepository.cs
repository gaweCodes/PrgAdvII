using System;
using System.Linq;
using System.Linq.Expressions;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.DataAccess.LINQ
{
    internal class LogRepository : LinqBaseRepository<ILogEntry>
    {
        public override void Add(ILogEntry entity)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext())
                Context.LogMessageAdd(entity.PoD, entity.Hostname, entity.Severity, entity.Message);
        }
        public override long Count()
        {
            using (Context = new SysInventoryLinqSqlContextDataContext(ConnectionString))
                return Context.GetTable<LogEntry>().LongCount();
        }
        public override long Count(Expression<Func<ILogEntry, bool>> whereExpression)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext(ConnectionString))
                return whereExpression != null ? Context.GetTable<LogEntry>().LongCount(whereExpression) : Context.GetTable<LogEntry>().LongCount();
        }
        public override void Delete(ILogEntry entity)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext())
            {
                Context.Logs.DeleteOnSubmit(Context.Logs.First(l => l.LogId == entity.Id));
                Context.SubmitChanges();
            }
        }

        public override IQueryable<ILogEntry> GetAll()
        {
            Context = new SysInventoryLinqSqlContextDataContext(ConnectionString);
            return Context.GetTable<LogEntry>();
        }
        public override IQueryable<ILogEntry> GetAll(Expression<Func<ILogEntry, bool>> whereExpression)
        {
            Context = new SysInventoryLinqSqlContextDataContext(ConnectionString);
            return whereExpression != null ? Context.GetTable<LogEntry>().Where(whereExpression) : Context.GetTable<LogEntry>();
        }
        public override ILogEntry GetSingle<TKey>(TKey pkValue)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext(ConnectionString))
            {
                var id = Guid.Parse(pkValue.ToString());
                return Context.GetTable<LogEntry>().First(x => x.Id == id);
            }
        }
        public override void Update(ILogEntry entity)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext())
                Context.LogClear(entity.Id);
        }
    }
}
