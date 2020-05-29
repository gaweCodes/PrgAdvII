using System;
using System.Linq;
using System.Linq.Expressions;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.DataAccess.LINQ
{
    internal class LogRepository : LinqBaseRepository<LogEntry>
    {
        public override void Add(LogEntry entity)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext())
                Context.LogMessageAdd(entity.PoD, entity.Hostname, entity.Severity, entity.Message);
        }
        public override long Count(Expression<Func<LogEntry, bool>> whereExpression)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext(ConnectionString))
                return Context.GetTable<LogEntry>().LongCount(x => x.Message != string.Empty);
        }
        public override void Delete(LogEntry entity)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext())
            {
                Context.Logs.DeleteOnSubmit(Context.Logs.First(l => l.LogId == entity.Id));
                Context.SubmitChanges();
            }
        }
        public override IQueryable<LogEntry> GetAll(Expression<Func<LogEntry, bool>> whereExpression)
        {
            Context = new SysInventoryLinqSqlContextDataContext(ConnectionString);
            return Context.GetTable<LogEntry>().Where(x => x.Severity > 4);
        }
        public override LogEntry GetSingle<TKey>(TKey pkValue)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext(ConnectionString))
            {
                var id = Guid.Parse(pkValue.ToString());
                return Context.GetTable<LogEntry>().First(x => x.Id == id);
            }
        }
        public override void Update(LogEntry entity)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext())
                Context.LogClear(entity.Id);
        }
    }
}
