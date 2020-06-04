using System;
using System.Linq;
using System.Linq.Expressions;

namespace SysInventory.LogMessages.DataAccess.Ef
{
    internal class AddressRepository : EfBaseRepository<Address>
    {
        public override void Add(Address entity)
        {
            throw new NotImplementedException();
        }

        public override long Count()
        {
            throw new NotImplementedException();
        }

        public override long Count(Expression<Func<Address, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Address entity)
        {
            throw new NotImplementedException();
        }
        public override IQueryable<Address> GetAll()
        {
            Context = new SysInventoryEntities();
            return Context.Addresses.Include("City");
        }
        public override IQueryable<Address> GetAll(Expression<Func<Address, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public override Address GetSingle<TKey>(TKey pkValue)
        {
            Context = new SysInventoryEntities();
            return Context.Addresses.Find(pkValue);
        }

        public override void Update(Address entity)
        {
            throw new NotImplementedException();
        }
    }
}
