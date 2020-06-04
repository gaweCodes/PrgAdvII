using System;
using System.Linq;
using System.Linq.Expressions;

namespace SysInventory.LogMessages.DataAccess.Ef
{
    internal class CustomerRepository : EfBaseRepository<Customer>
    {
        public override void Add(Customer entity)
        {
            using (Context = new SysInventoryEntities())
            {

                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.Now;
                entity.AddressFk = entity.Address.Id;
                entity.Address = null;
                entity.AddressTypeFk = entity.AddressType.Id;
                entity.AddressType = null;
                Context.Customers.Add(entity);
                Context.SaveChanges();
            }
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
            entity.Address = null;
            entity.AddressType = null;
            Context.Customers.Remove(entity);
            Context.SaveChanges();
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
            using (Context = new SysInventoryEntities())
            {
                return Context.Customers.Find(pkValue);
            }
        }
        public override void Update(Customer entity)
        {
            using (Context = new SysInventoryEntities())
            {
                var found = Context.Customers.Find(entity.Id);
                if(found == null) return;
                found.Mail = entity.Mail;
                found.Name = entity.Name;
                found.AddressFk = entity.Address.Id;
                found.AddressTypeFk = entity.AddressType.Id;
                Context.SaveChanges();
            }
        }
    }
}
