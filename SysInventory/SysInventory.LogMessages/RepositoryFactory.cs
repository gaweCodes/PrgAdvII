using System;
using SysInventory.LogMessages.DataAccess;
using SysInventory.LogMessages.DataAccess.AdoNet;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages
{
    internal class RepositoryFactory
    {
        public IRepositoryBase<ILogEntry> GetLogEntryRepository(string connectionStrategy)
        {
            switch (connectionStrategy)
            {
                case "AdoNet":
                    return new LogRepository();
                case "LINQ":
                    return new DataAccess.LINQ.LogRepository();
                case "EF":
                    return new DataAccess.Ef.LogRepository();
                default:
                    throw new NotSupportedException("This strategy is not supported: " + connectionStrategy);
            }
        }
        public IRepositoryBase<ILocation> GetLocationRepository(string connectionStrategy)
        {
            switch (connectionStrategy)
            {
                case "AdoNet":
                    return new LocationRepository();
                case "LINQ":
                    return new DataAccess.LINQ.LocationRepository();
                case "EF":
                    return new DataAccess.Ef.LocationRepository();
                default:
                    throw new NotSupportedException("This strategy is not supported: " + connectionStrategy);
            }
        }
    }
}
