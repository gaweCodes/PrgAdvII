using System;
using System.Linq;
using System.Linq.Expressions;

namespace SysInventory.LogMessages.DataAccess.Ef
{
    class CustomerRepository : EfBaseRepository<Customer>
    {
        public override void Add(Customer entity)
        {
            throw new NotImplementedException();
        }
        public override long Count()
        {
            using (Context = new SysInventoryEntities())
            {
                return Context.Customers.LongCount();
            }
        }
        public override long Count(Expression<Func<Customer, bool>> whereExpression)
        {
            using (Context = new SysInventoryEntities())
            {
                return Context.Customers.LongCount(whereExpression);
            }
        }
        public override void Delete(Customer entity)
        {
            throw new NotImplementedException();
        }
        public override IQueryable<Customer> GetAll()
        {
            Context = new SysInventoryEntities();
            return Context.Customers.Include("Address");
        }
        public override IQueryable<Customer> GetAll(Expression<Func<Customer, bool>> whereExpression)
        {
            Context = new SysInventoryEntities();
            return Context.Customers.Include("Address").Where(whereExpression);
        }

        public override Customer GetSingle<TKey>(TKey pkValue)
        {
            throw new NotImplementedException();
        }

        public override void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
