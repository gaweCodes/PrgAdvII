using SysInventory.LogMessages.DataAccess;
using SysInventory.LogMessages.DataAccess.AdoNet;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages
{
    internal class RepositoryFactory
    {
        public IRepositoryBase<LogEntry> GetLogEntryRepository(string connectionStrategy)
        {
            if (connectionStrategy == "AdoNet")
            {
                return new LogRepository();
            }
            return new DataAccess.LINQ.LogRepository();
        }
        public IRepositoryBase<Location> GetLocationRepository(string connectionStrategy)
        {
            if (connectionStrategy == "AdoNet")
            {
                return new LocationRepository();
            }
            return new DataAccess.LINQ.LocationRepository();
        }
    }
}
