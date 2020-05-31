using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SysInventory.LogMessages.DataAccess.Ef
{
    internal abstract class EfBaseRepository<T> : IRepositoryBase<T> where T : class
    {
        protected SysInventoryEntities Context;
        public abstract void Add(T entity);
        public abstract long Count();
        public long Count(string whereCondition, Dictionary<string, object> parameterValues) =>
            throw new NotSupportedException("This method is not supported for Linq");
        public abstract long Count(Expression<Func<T, bool>> whereExpression);
        public abstract void Delete(T entity);
        public abstract IQueryable<T> GetAll();
        public IQueryable<T> GetAll(string whereCondition, Dictionary<string, object> parameterValues) =>
            throw new NotSupportedException("This method is not supported for Linq");
        public abstract IQueryable<T> GetAll(Expression<Func<T, bool>> whereExpression);
        public abstract T GetSingle<TKey>(TKey pkValue);
        public abstract void Update(T entity);
        public void CleanUp() => Context.Dispose();
    }
}
