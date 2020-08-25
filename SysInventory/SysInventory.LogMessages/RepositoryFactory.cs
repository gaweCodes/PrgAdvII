using System;
using Autofac;
using SysInventory.LogMessages.DataAccess;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages
{
    internal class RepositoryFactory
    {
        private readonly IContainer _container;
        public RepositoryFactory(IContainer container)
        {
            this._container = container;
        }

        public IRepositoryBase<ILogEntry> GetLogEntryRepository(string connectionStrategy)
        {
            switch (connectionStrategy)
            {
                case "AdoNet":
                    return _container.ResolveNamed<IRepositoryBase<ILogEntry>>("LogAdoNet");
                case "LINQ":
                    return _container.ResolveNamed<IRepositoryBase<ILogEntry>>("LogLINQ");
                case "EF":
                    return _container.ResolveNamed<IRepositoryBase<ILogEntry>>("LogEf");
                default:
                    throw new NotSupportedException("This strategy is not supported: " + connectionStrategy);

            }
        }
        public IRepositoryBase<ILocation> GetLocationRepository(string connectionStrategy)
        {
            switch (connectionStrategy)
            {
                case "AdoNet":
                    return _container.ResolveNamed<IRepositoryBase<ILocation>>("LocationAdoNet");
                case "LINQ":
                    return _container.ResolveNamed<IRepositoryBase<ILocation>>("LocationLINQ");
                case "EF":
                    return _container.ResolveNamed<IRepositoryBase<ILocation>>("LocationEf");
                default:
                    throw new NotSupportedException("This strategy is not supported: " + connectionStrategy);
            }
        }
    }
}
