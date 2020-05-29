using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SysInventory.LogMessages.Properties;

namespace SysInventory.LogMessages.DataAccess.LINQ
{
    internal abstract class LinqBaseRepository<T> : IRepositoryBase<T> where T : class
    {
        protected string ConnectionString { get; }
        protected SysInventoryLinqSqlContextDataContext Context;
        protected LinqBaseRepository() => ConnectionString = Settings.Default.ConnectionString;
        public abstract void Add(T entity);
        public long Count()
        {
            using (Context = new SysInventoryLinqSqlContextDataContext(ConnectionString))
                return Context.GetTable<T>().LongCount();
        }
        public long Count(string whereCondition, Dictionary<string, object> parameterValues) =>
            throw new NotSupportedException("This method is not supported for Linq");
        public abstract long Count(Expression<Func<T, bool>> whereExpression);
        public virtual void Delete(T entity)
        {
            using (Context = new SysInventoryLinqSqlContextDataContext(ConnectionString))
            {
                Context.GetTable<T>().DeleteOnSubmit(entity);
                Context.SubmitChanges();
            }
        }
        public IQueryable<T> GetAll()
        {
            Context = new SysInventoryLinqSqlContextDataContext(ConnectionString);
            return Context.GetTable<T>();
        }
        public IQueryable<T> GetAll(string whereCondition, Dictionary<string, object> parameterValues) =>
            throw new NotSupportedException("This method is not supported for Linq");
        public abstract IQueryable<T> GetAll(Expression<Func<T, bool>> whereExpression);
        public abstract T GetSingle<TKey>(TKey pkValue);
        public abstract void Update(T entity);
    }
}
