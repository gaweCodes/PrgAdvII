using System;
using System.Linq;
using System.Linq.Expressions;

namespace SysInventory.LogMessages.DataAccess.Ef
{
    internal class WindowRepository : EfBaseRepository<WindowFunction>
    {
        public override void Add(WindowFunction entity) => throw new NotSupportedException();
        public override long Count() => throw new NotSupportedException();
        public override long Count(Expression<Func<WindowFunction, bool>> whereExpression) => throw new NotSupportedException();
        public override void Delete(WindowFunction entity) => throw new NotSupportedException();
        public override IQueryable<WindowFunction> GetAll()
        {
            Context = new SysInventoryEntities();
            return Context.WindowFunctions;
        }
        public override IQueryable<WindowFunction> GetAll(Expression<Func<WindowFunction, bool>> whereExpression) => throw new NotSupportedException();
        public override WindowFunction GetSingle<TKey>(TKey pkValue) => throw new NotSupportedException();
        public override void Update(WindowFunction entity) => throw new NotSupportedException();
    }
}
