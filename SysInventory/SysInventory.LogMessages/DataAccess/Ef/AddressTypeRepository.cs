using System;
using System.Linq;
using System.Linq.Expressions;

namespace SysInventory.LogMessages.DataAccess.Ef
{
    internal class AddressTypeRepository : EfBaseRepository<AddressType>
    {
        public override void Add(AddressType entity)
        {
            throw new NotImplementedException();
        }

        public override long Count()
        {
            throw new NotImplementedException();
        }

        public override long Count(Expression<Func<AddressType, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public override void Delete(AddressType entity)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<AddressType> GetAll()
        {
            Context = new SysInventoryEntities();
            return Context.AddressTypes;
        }

        public override IQueryable<AddressType> GetAll(Expression<Func<AddressType, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public override AddressType GetSingle<TKey>(TKey pkValue)
        {
            Context = new SysInventoryEntities();
            return Context.AddressTypes.Find(pkValue);
        }

        public override void Update(AddressType entity)
        {
            throw new NotImplementedException();
        }
    }
}
